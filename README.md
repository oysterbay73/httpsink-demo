# httpsink-demo

# Introduction

Contains projects to test serilog http sink https://github.com/FantasticFiasco/serilog-sinks-http. Solution has been created to demonstrate
an issue with .net framework web project logging. Other successful project types are included for reference.

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

Legacy .net framework (4.7.2) web application, also generated with the template included in Visual Studio, with logging added. For simplicity
the logger has been applied directly to the home controller. The site runs but the logging does not function correctly. The buffer bookmark file
quickly becomes locked so initial logs are written, but then these get blocked. Error output is self logged can be viewed in debug output.

## Executing and Debugging

* Recommended that Visual studio is used, set start up project as required and debug (for web projects this will run in iis express).
* Note that all logger configurations are hard-coded in the applicable project ```Program.cs```, except the framework web project where it is instead in ```Controllers/HomeController.cs```
* The failing .net framework web project anecdotally appears to fail more quickly then when running in iis over iis express. To run in IIS, bind an IIS site to the site root. To debug attach to the iis worker process.
* No valid http target is provided so all requests to push the logs from the files to the http target will fail unless you provide a valid target (```requestUri```).
* It is not necessary to provide a valid http target to demonstrate the failure in the .net framework web project.
* When running the framework web the self logged error ```Serilog.Sinks.Http.Private.Network.HttpLogShipper: System.IO.IOException: The process cannot access the file 'buffer.bookmark' because it is being used by another process``` will soon occur.
* The error is not present when running the other projects, although without a valid http target other errors are logged.
* If valid http targets are provided then the 3 successful projects will log to them successfully. The failing project will normally log the first batch only before the file is locked.
Have also witnessed the same log being sent over and over again.
* Playing around with the file location (```bufferBaseFileName```), permissions, and ```bufferFileShared``` option does not resolve the issue.
 





