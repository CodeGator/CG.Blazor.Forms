# CG.Blazor.Forms: 

---
[![Build Status](https://dev.azure.com/codegator/CG.Blazor.Forms/_apis/build/status/CodeGator.CG.Blazor.Forms?branchName=main)](https://dev.azure.com/codegator/CG.Blazor.Forms/_build/latest?definitionId=72&branchName=main)
[![Github docs](https://img.shields.io/static/v1?label=Documentation&message=online&color=blue)](https://codegator.github.io/CG.Blazor.Forms/index.html)
[![NuGet downloads](https://img.shields.io/nuget/dt/CG.Blazor.Forms.svg?style=flat)](https://nuget.org/packages/CG.Blazor.Forms)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/codegator/CG.Blazor.Forms/72)
[![Github discussion](https://img.shields.io/badge/Discussion-online-blue)](https://github.com/CodeGator/CG.Blazor.Forms/discussions)
[![CG.Blazor.Forms on fuget.org](https://www.fuget.org/packages/CG.Blazor.Forms/badge.svg)](https://www.fuget.org/packages/CG.Blazor.Forms)

#### What does it do?
The package contains server side Blazor forms extensions used by other CodeGator packages.

#### Commonly used types:
* Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions
* CG.Blazor.Forms.Components.DynamicForm
* CG.Blazor.Forms.Attributes.FormGeneratorAttribute
* CG.Blazor.Forms.Attributes.FormValidationAttribute
* CG.Blazor.Forms.Attributes.RenderObjectAttribute
* CG.Blazor.Forms.Attributes.Validation.RenderDataAnnotationsValidatorAttribute
* CG.Blazor.Forms.Attributes.Validation.RenderValidationSummaryAttribute
* CG.Blazor.Forms.Attributes.Html.HtmlAttribute
* CG.Blazor.Forms.Attributes.Html.RenderCheckBoxAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputColorAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputDateAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputEmailAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputMonthAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputNumberAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputPasswordAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputRangeAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputSelectAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputTelephoneAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputTextAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputTimeAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputUrlAttribute
* CG.Blazor.Forms.Attributes.Html.RenderInputWeekAttribute
* CG.Blazor.Forms.Attributes.Html.RenderMeterAttribute
* CG.Blazor.Forms.Attributes.Html.RenderProgressAttribute
* CG.Blazor.Forms.Attributes.Html.RenderRadioGroupAttribute
* CG.Blazor.Forms.Attributes.Html.RenderTextAreaAttribute

#### What platform(s) does it support?
* .NET 6.x or higher

#### How do I install it?
The binary is hosted on [NuGet](https://www.nuget.org/packages/CG.Blazor.Forms). To install the package using the NuGet package manager:

PM> Install-Package CG.Blazor.Forms

#### How do I contact you?
If you've spotted a bug in the code please use the project Issues [HERE](https://github.com/CodeGator/CG.Blazor.Forms/issues)

We have a discussion group [HERE](https://github.com/CodeGator/CG.Blazor.Forms/discussions)

#### Is there any documentation?
There is developer documentation [HERE](https://codegator.github.io/CG.Blazor.Forms/)

We also blog about projects like this one on our website, [HERE](http://www.codegator.com)

---

#### How do I get started?

There is a working quick start sample [HERE](https://github.com/CodeGator/CG.Blazor.Forms/tree/main/samples/CG.Blazor.Forms.QuickStart) 

Steps to get started:

1. Create a Blazor project to get started.

2. Add the CG.Blazor.Forms NUGET package to the project.

3. Add `@using CG.Blazor.Forms.Attributes` to the _Imports.razor file.

4. Add `<DynamicForm Model="@Model" OnValidSubmit="OnValidSubmit"/>` to the razor component where you want your dynamic form generated. Note that `Model` is a reference to your POCO object, and `OnValidSubmit` is a reference to your form's submit handler.

5. Add `services.AddFormGeneration();` to the `ConfigureServices` method of the `Startup` class.

6. Create your model type. Use attributes from the NUGET package to decorate any properties you want to be rendered on the form. Here is an example:

```
[RenderValidationSummary()]
[RenderFluentValidationValidator]
public class MyForm
{
	[RenderInputText]
	[Required]
	public string FirstName { get; set; }

	[RenderInputText]
	[Required]
	public string LastName { get; set; }
}
```

That's pretty much it! You can, of course, get fancier, but that's up to you.




