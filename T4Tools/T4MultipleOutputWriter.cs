using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace T4Tools
{
    public class T4MultipleOutputWriter 
    {
        private List<string> _savedOutputs = new List<string>();
        private Engine _engine = new Engine();
        private ITextTemplatingEngineHost _host = null;
        private StringBuilder _generatationEnvironment = null;

        public T4MultipleOutputWriter(ITextTemplatingEngineHost Host, StringBuilder GenerationEnvironment)
        {
            if (Host == null)
                throw new ArgumentNullException("HostNotFound");
            if (GenerationEnvironment == null)
                throw new ArgumentNullException("GenerationEnvironmentNotFound");

            _host = Host;
            _generatationEnvironment = GenerationEnvironment;
        }

        public void DeleteOldOutputs()
        {
            ProjectItem templateProjectItem = _getTemplateProjectItem();
            foreach (ProjectItem childProjectItem in templateProjectItem.ProjectItems)
            {
                if (!_savedOutputs.Contains(childProjectItem.Name))
                    childProjectItem.Delete();
            }
        }

        public void ProcessTemplate(string templateFileName, string outputFileName)
        {
            string templateDirectory = Path.GetDirectoryName(_host.TemplateFile);
            string outputFilePath = Path.Combine(templateDirectory, outputFileName);

            string template = File.ReadAllText(_host.ResolvePath(templateFileName));
            string output = _engine.ProcessTemplate(template, _host);
            File.WriteAllText(outputFilePath, output);

            ProjectItem templateProjectItem = _getTemplateProjectItem();
            templateProjectItem.ProjectItems.AddFromFile(outputFilePath);

            _savedOutputs.Add(outputFileName);
        }

        public void SaveOutput(string outputFileName)
        {
            string templateDirectory = Path.GetDirectoryName(_host.TemplateFile);
            string outputFilePath = Path.Combine(templateDirectory, outputFileName);

            File.WriteAllText(outputFilePath, _generatationEnvironment.ToString());
            _generatationEnvironment = new StringBuilder();

            ProjectItem templateProjectItem = _getTemplateProjectItem();
            templateProjectItem.ProjectItems.AddFromFile(outputFilePath);

            _savedOutputs.Add(outputFileName);
        }

        private ProjectItem _getTemplateProjectItem()
        {
            Project dteProject = _getTemplateProject();

            IVsProject vsProject = _dteProjectToVsProject(dteProject);

            int iFound = 0;
            uint itemId = 0;
            VSDOCUMENTPRIORITY[] pdwPriority = new VSDOCUMENTPRIORITY[1];
            int result = vsProject.IsDocumentInProject(_host.TemplateFile, out iFound, pdwPriority, out itemId);

            if (result != VSConstants.S_OK)
                throw new Exception("Unexpected error calling IVsProject.IsDocumentInProject");
            if (iFound == 0)
                throw new Exception("Cannot retrieve ProjectItem for template file");
            if (itemId == 0)
                throw new Exception("Cannot retrieve ProjectItem for template file");

            Microsoft.VisualStudio.OLE.Interop.IServiceProvider itemContext = null;
            result = vsProject.GetItemContext(itemId, out itemContext);
            if (result != VSConstants.S_OK)
                throw new Exception("Unexpected error calling IVsProject.GetItemContext");
            if (itemContext == null)
                throw new Exception("IVsProject.GetItemContext returned null");

            ServiceProvider itemContextService = new ServiceProvider(itemContext);
            ProjectItem templateItem = (ProjectItem)itemContextService.GetService(typeof(ProjectItem));
            Debug.Assert(templateItem != null, "itemContextService.GetService returned null");

            return templateItem;
        }

        private Project _getTemplateProject()
        {
            IServiceProvider hostServiceProvider = (IServiceProvider)_host;
            if (hostServiceProvider == null)
                throw new Exception("Host property returned unexpected value (null)");

            DTE dte = (DTE)hostServiceProvider.GetService(typeof(DTE));
            if (dte == null)
                throw new Exception("Unable to retrieve EnvDTE.DTE");

            Array activeSolutionProjects = (Array)dte.ActiveSolutionProjects;
            if (activeSolutionProjects == null)
                throw new Exception("DTE.ActiveSolutionProjects returned null");

            Project dteProject = (Project)activeSolutionProjects.GetValue(0);
            if (dteProject == null)
                throw new Exception("DTE.ActiveSolutionProjects[0] returned null");

            return dteProject;
        }

        private static IVsProject _dteProjectToVsProject(EnvDTE.Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            string projectGuid = null;

            // DTE does not expose the project GUID that exists at in the msbuild project file.        
            // Cannot use MSBuild object model because it uses a static instance of the Engine,         
            // and using the Project will cause it to be unloaded from the engine when the         
            // GC collects the variable that we declare.       
            using (XmlReader projectReader = XmlReader.Create(project.FileName))
            {
                projectReader.MoveToContent();
                object nodeName = projectReader.NameTable.Add("ProjectGuid");
                while (projectReader.Read())
                {
                    if (Object.Equals(projectReader.LocalName, nodeName))
                    {
                        projectGuid = (string)projectReader.ReadElementContentAsString();
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(projectGuid))
                throw new Exception("Unable to find ProjectGuid element in the project file");

            Microsoft.VisualStudio.OLE.Interop.IServiceProvider dteServiceProvider =
                (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)project.DTE;
            IServiceProvider serviceProvider = new ServiceProvider(dteServiceProvider);
            IVsHierarchy vsHierarchy = VsShellUtilities.GetHierarchy(serviceProvider, new Guid(projectGuid));

            IVsProject vsProject = (IVsProject)vsHierarchy;

            if (vsProject == null)
                throw new ArgumentException("Project is not a VS project.");

            return vsProject;
        }
    }
}
