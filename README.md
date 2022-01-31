# httpsink-demo

# Introduction

Contains projects to test serilog http sink https://github.com/FantasticFiasco/serilog-sinks-http. Solution originally created to demonstrate
an issue with .net framework web project logging, now partially resolved. Other successful project types also included for reference.

# About the project

The repo holds the following projects.

## ConsoleApp 

Core (.net 6.0) Console app project. Continues logging message until escape key pressed. Runs and logs successfully with no issues.

## ConsoleNetFw

A legacy .net framework (4.7.2) version of the console app. Runs and logs successfully with no issues.

## WebApp

Core (.net 6.0) web application.
This is a copy of the starter site, generated with the template included in Visual Studio. The only addition is the logging implementation.
Runs and logs successfully with no issues.

## WebNetFw

Legacy .net framework (4.7.2) web application, also generated with the template included in Visual Studio, with logging added. Logger was originally
created in contoller which mimicked injected logger where problem originally identified. Now moved to global and now runs and logs successfully without file conflict.
However, when running in IIS there is still an issue with invalid characters being written to the file after re-start (app pool recycle or IIS reset).

## Executing and Debugging

* Recommended that Visual studio is used, set start up project as required and debug (for web projects this will run in iis express).
* Remaining error in .net framework web app only applies when bound to IIS however. To run in IIS, bind an IIS site to the site root. To debug attach to the iis worker process.
* Note that all logger configurations are hard-coded in the applicable project ```Program.cs```, except the framework web project where it is instead in ```Global.asax.cs```
* No valid http target is provided so all requests to push the logs from the files to the http target will fail unless you provide a valid target (```requestUri```).
* It is not necessary to provide a valid http target to demonstrate the failure in the .net framework web project.
* When running the framework web project, when the original error occurred, this was ```Serilog.Sinks.Http.Private.Network.HttpLogShipper: System.IO.IOException: The process cannot access the file 'buffer.bookmark' because it is being used by another process```.
* Note that without a valid http target other expected errors are logged.


## Demonstrating the error in WebNetFw with IIS

### Enabling IIS
* IIS is available on all Windows editions but it may need to be enabled, my example is with IIS version 10 on Windows 10.
* Easiest way to install IIS if not installed already is from command prompt. Type ```OptionalFeatures``` and enter to bring up the windows features UI. Enable
'Internet Information Services' and options beneath.
* You will at least need to enable 'Web management tools' > 'IIS Management Console' and 'World Wide Web Services'. I think this enables default options beneath
which should suffice, it toggles the same existing options on and off in my instance.

### Setting up the site
* The IIS UI is named 'Internet Information Services (IIS) Manager' and can be found by searching for 'iis'.
* If you have no other sites configured in IIS then you can use the default website. Otherwise you can create a new site.
* You can view which app pool the site is running under from 'Basic settings' under the Action menu on the right.
* From the 'Application Pools' section, check that the app pool associated with the site is running with .NET CLR v4.0.* in Integrated managed pipeline mode. It is fine to run under app pool identity.
* Also from 'Basic settings' you can set the physical path to the site, set this to the root source code location of the 'WebNetFw' site.
* You can now use the 'Test Settings' button from 'Basic Settings' to test connectivity, Authentication should be OK showing pass through authentication, Authorization may show a warning which is not an issue.
* In file explorer grant the app pool full control over the folder where writing logs to (so the 'Logs' folder within the site). E.g. default app pool is 'IIS AppPool\DefaultAppPool').
* If you now build the site in Visual Studio or from the command line, then you sholuld now be able to browse the site, assuming the default website, this will be on http://localhost:80.
* This site will load and function correctly. You can see logs being written to the file. Other (expected) errors will be report to debug output as there is no valid http target.
* Now, within IIS, go the 'Application Pools' section and recycle the pool used by the site (from the right hand Actions menu).
* You will see that the application continues to log successfully to the file.
* However, look at the file in more detail and invalid characters are written at the start of the first line after the recycle.
* E.g. use notepad++ with language set to json and the offending line will be highlighted as invalid json.
* Because the json is invalid, logs sent to a valid http endpoint will get stuck at the problem line, I am using a Splunk endpoint (not included in project) which reports back the failure.



