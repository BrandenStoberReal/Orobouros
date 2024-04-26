# The Orobouros Framework
Orobouros is a C# framework for scraping the web. Many attempts to do this have been created in various languages, but a different approach is taken with Orobouros due to the patented OrobourosModule™ system that allows any person to write their own plugin for any website.

# Installation
Orobouros is available [as a NuGet package](https://www.nuget.org/packages/Orobouros) and from the ``Github Actions`` page. Keep in mind the pre-compiled builds on GitHub do not include dependencies. If you prefer the .NET CLI, you can also simply run:

```
dotnet add package Orobouros
```

On its own, Orobouros does nothing and needs modules to function. A list of publically available modules for download is listed [on the GitHub repository](https://github.com/BrandenStoberReal/Orobouros-Public-Modules).

# Building
If you insist on compiling this yourself, all you need is ``.NET 8 Core``. I would not recommend taking advantage of the tests, as they require specific configurations I use in debugging.

# Development
Take a look at the TestModule project included in this repo to get a general idea on how to use this framework. XML annotations are also provided. At some point I will create a wiki with relevant information, but for now the core functionality takes priority. Obfuscated code is allowed (as in the framework won't refuse to execute it) but incredibly discouraged due to malware concerns. If you really feel the need to keep your source code hidden, just don't share your module.

Example code to submit a scrape request to the loaded module stack:
```csharp
ScrapingManager.InitializeModules(); // Only call this once at the entry point of your application
List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Text }; // Content you want to request from the modules. How this is handled is entirely dependent on the module's developer.
ModuleData? data = ScrapingManager.ScrapeURL("https://www.test.com/posts/posthere", requestedInfo); // Perform scrape request and wait for the returned data.
ScrapingManager.FlushSupplementaryMethods(); // Stop background methods. This should be called at least once when the application is exiting.
```

Example code to return a simple line of text from a module's scrape method:
```csharp
ModuleData data = new ModuleData();
ProcessedScrapeData exampleInstance = new ProcessedScrapeData(ModuleContent.Text, parameters.URL, "Hello World!");
data.Content.Add(exampleInstance);
return data;
```

Please consult the XML documentation or the TestModule project for further code examples.

# Copyright
This repository holds no responsibility over any modules programmers develop for this framework. No copywritten content is included in this repo and will never be. If someone has made a module for your website and you don't like it, I cannot help you. You must get in contact with them to resolve such matters. This also applies to potentially illicit/illegal content scraped with modules created by the community. 

# TODO:
- [x] Dynamic module loading
- [x] Raw HTTP support
- [x] Downloader service
- [x] Attribute scanning
- [x] Custom attributes
- [x] Module init method
- [x] Module supplementary methods
- [x] Module scrape method
- [x] Module options
- [x] Module return data
- [x] Module GUIDs
- [x] Custom library support
- [x] ~~Referenced library support~~ **Deprecated**
- [x] SQLite support
- [x] Dynamic database support
- [ ] Website API support (separate from raw HTTP)
- [ ] Cross-module support
- [x] XML annotations
- [ ] Module security checks
- [x] Module sanity checks
- [x] Multiple modules for same website support
- [x] Improved module error handling
- [x] SQlite module integration
- [x] Public module downloader tool
- [ ] Switch to protobuf
- [ ] General framework configuration class
- [ ] Cross-language support (extremely advanced)
- [ ] Data language translation toolkit
- [ ] Overhaul download class & integrate better
- [x] Logging overhaul
- [x] Module developer web toolkit
- [ ] Framework-level exception handling
- [x] Bulk data downloading functions (stored in RAM)

# Credits
- Branden Stober - Main Project Lead
- ImSoupp - Reflection Help & Database Help
- CTAG - Database Help
