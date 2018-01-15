# StackOverflow Client - WPF Application

This is a complex C# solution/environment for testing new ideas, features, frameworks etc.
Solution is based on WPF/MVVM application with Unity container which provides Dependency Injection.
This approach allows you to replace almost any solution component with another one that implements specified interface.

**This is over complicated solution with great emphasis on modularity which allows to test lot of different approaches (e.g. testing few MVVM frameworks in one application)*

# Frameworks and solution used in project:
##### **1. Core/Common:**
* Unity 
* NLog
* NUnit + Moq
* Newtonsoft.JSON
	
##### **2. Projects done:**
* WPF/MVVM application
* Entity Framework (Code First) + SQLite
* REST API calls with System.Net.Http
* Setup project (with MSC VS 2017 Installer Projects extension)
	
##### **3. To do / To test:**
- WCF Service
- MVVM Frameworks (DevExpress, MVVMLight, Caliburn?)
- Micro ORMs (NHibernate, Dapper)
- Continous Integration
---
## Neverending story - thing to do in every project:
- Handling exceptions and Logger
- Unit Tests