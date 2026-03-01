module FSharp.SourceDjinn.Tests.AstParserTests

open NUnit.Framework
open FSharp.SourceDjinn

[<Test>]
let ``Detects EntryPoint attribute on top-level function`` () =
    let source = """
module Program

[<FSharp.SourceDjinn.EntryPoint>]
let run argv = 0
"""
    let result = EntryPointDetector.detect "/test.fs" source
    Assert.That(result.IsSome, Is.True)
    Assert.That(result.Value.ModuleName, Is.EqualTo("Program"))
    Assert.That(result.Value.FunctionName, Is.EqualTo("run"))

[<Test>]
let ``No entry point attribute returns None`` () =
    let source = """
namespace MyApp

type Person = { Name: string; Age: int }
"""
    let result = EntryPointDetector.detect "/test.fs" source
    Assert.That(result.IsNone, Is.True)

[<Test>]
let ``Extracts correct module name and function name`` () =
    let source = """
module MyApp.Program

[<FSharp.SourceDjinn.EntryPoint>]
let main argv = 0
"""
    let result = EntryPointDetector.detect "/test.fs" source
    Assert.That(result.IsSome, Is.True)
    Assert.That(result.Value.ModuleName, Is.EqualTo("MyApp.Program"))
    Assert.That(result.Value.FunctionName, Is.EqualTo("main"))

[<Test>]
let ``Works with short EntryPoint attribute name`` () =
    let source = """
module Program

open FSharp.SourceDjinn.TypeModel

[<EntryPoint>]
let run argv = 0
"""
    let result = EntryPointDetector.detect "/test.fs" source
    Assert.That(result.IsSome, Is.True)
    Assert.That(result.Value.ModuleName, Is.EqualTo("Program"))
    Assert.That(result.Value.FunctionName, Is.EqualTo("run"))
