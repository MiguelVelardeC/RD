<%@ Application Language="C#" %>

<script runat="server">

    /// <summary>
    /// If the task manageris running any tasks
    /// </summary>
    static bool runningTasks;
    /// <summary>
    /// The number of times the manager is ordered to start a task... and if a task had already begun,
    /// thus possibly overlapping tasks
    /// </summary>
    static int noOverlapTaskTry;
    /// <summary>
    /// The lock object used with monitors to ensure that only one task at a time is executed
    /// </summary>
    static object lockObject = new object();
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        // Configure the logging system
        log4net.Config.XmlConfigurator.Configure();

        log4net.ILog log = log4net.LogManager.GetLogger("Standard");

        log.Info("Application is starting");

        // ---------------------------------------------------
        // - Installation of the Task Manager
        // ---------------------------------------------------
        Artexacta.App.Utilities.TaskManager.Manager theManager =
            Artexacta.App.Utilities.TaskManager.BLL.ManagerBLL.GetCurrentmanager();
        if (theManager != null && theManager.Status)
        {
            //Task Manager SetUp
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadFunc));
            thread.IsBackground = true;
            thread.Name = Artexacta.App.Constants.THREAD_NAME;
            thread.Start();
            log.Info("Background thread started for application");
        }
        
        // Make sure that the administrator user record are created
        Artexacta.App.Security.BLL.SecurityBLL.SetUpAccessControlPermisions();

        log.Debug("Access control permissions set. Initializing configuration");
    }

    void ThreadFunc()
    {
        Artexacta.App.Utilities.TaskManager.Manager theManager =
            Artexacta.App.Utilities.TaskManager.BLL.ManagerBLL.GetCurrentmanager();

        System.Timers.Timer t = new System.Timers.Timer();
        t.Elapsed += new System.Timers.ElapsedEventHandler(TimerWorker);

        // time in ms: every hour
        t.Interval = theManager.SleepTimeSeconds * 1000;
        t.Enabled = true;
        t.AutoReset = true;
        t.Start();
    }

    void TimerWorker(object sender, System.Timers.ElapsedEventArgs e)
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Standard");
        Artexacta.App.Utilities.TaskManager.Manager theManager =
            Artexacta.App.Utilities.TaskManager.BLL.ManagerBLL.GetCurrentmanager();
        try
        {
            if (runningTasks)
            {
                noOverlapTaskTry++;
                log.Info("Tasks are currently being executed");
                if (noOverlapTaskTry > theManager.NumberOfOverlapsAllowed)
                {
                    log.Debug("Sorry, Too Much overlapping, must kill the tasks");
                    Artexacta.App.Utilities.TaskManager.TaskManager.pleaseStop = true;
                }
                return;
            }
        }
        catch (Exception ex)
        {
            log.Error("There was an error verifying tasks", ex);
        }

        System.Threading.Monitor.Enter(lockObject);

        if (!runningTasks)
        {
            try
            {
                log.Info("Start tasks execution");
                noOverlapTaskTry = 0;
                runningTasks = true;
                Artexacta.App.Utilities.TaskManager.TaskManager.pleaseStop = false;
                Artexacta.App.Utilities.TaskManager.TaskManager.executeTasks();
                log.Debug("Tasks executed correctly");
            }
            catch (Exception ex)
            {
                log.Error("Error executing tasks", ex);
            }
            runningTasks = false;

        }

        System.Threading.Monitor.Exit(lockObject);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e)
    {
        // Attempt to log the error in the application log file. 
        log4net.ILog log = log4net.LogManager.GetLogger("Standard");
        try
        {
            Exception objError = HttpContext.Current.Server.GetLastError().GetBaseException();
            if (objError != null)
            {
                log.Error("Unhandled error in application", objError);
                Application["LAST_ERROR_MESSAGE"] = objError.Message;
                if (objError.InnerException != null)
                {
                    log.Error("Internal error unhandled error in application", objError.InnerException);
                    Application["LAST_ERROR_INNERMESSAGE"] += "<br />" + objError.InnerException.Message;
                }
            }

            if (objError is System.Web.HttpException)
            {
                System.Web.HttpException qq = (System.Web.HttpException)objError;
                log.Error("Last exception was HTTP Exception. Error: " + qq.ErrorCode.ToString());
                log.Error("Last exception was HTTP Exception. Error: " + qq.Message);
                return;
            }
        }
        catch(Exception q)
        {
            log.Error("Unhandled error in application", q);
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
