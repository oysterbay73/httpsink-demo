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
created in contoller which mimicked injected logger where problem originally identified. Now moved to global and now runs amd logs successfully with no issue.

## Executing and Debugging

* Recommended that Visual studio is used, set start up project as required and debug (for web projects this will run in iis express).
* Remaining error in .net framework web app only applies when bound to IIS however. To run in IIS, bind an IIS site to the site root. To debug attach to the iis worker process.
* Note that all logger configurations are hard-coded in the applicable project ```Program.cs```, except the framework web project where it is instead in ```Global.asax.cs```
* No valid http target is provided so all requests to push the logs from the files to the http target will fail unless you provide a valid target (```requestUri```).
* It is not necessary to provide a valid http target to demonstrate the failure in the .net framework web project.
* When running the framework web project, when the original error occurred, this was ```Serilog.Sinks.Http.Private.Network.HttpLogShipper: System.IO.IOException: The process cannot access the file 'buffer.bookmark' because it is being used by another process```.
* Error now occurs on IIS recycle. An invalid character is written to the log file at the start of the line on the 1st log record after the recycle. If you have a valid http target set then the log send gets stuck here. 
* To view, for example, use Notepad++ with language set to json, error line will be highlighted.
* Note that without a valid http target other expected errors are logged.


