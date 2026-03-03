# FSharp.SourceDjinn  
[![Serde.FS](https://img.shields.io/nuget/vpre/FSharp.SourceDjinn.svg?label=FSharp.SourceDjinn)](https://www.nuget.org/packages/FSharp.SourceDjinn/)
[![Serde.FS.SourceGen](https://img.shields.io/nuget/vpre/FSharp.SourceDjinn.TypeModel.svg?label=FSharp.SourceDjinn.TypeModel)](https://www.nuget.org/packages/FSharp.SourceDjinn.TypeModel/)

A lightweight engine for extracting a **simplified, stable type model** from F# source code. Djinn is designed for source generators that scan code for **custom marker attributes** and generate code based on the types that carry them.

Djinn focuses on **syntax**, not semantics. It parses F# source files and produces a backend‑agnostic model of modules, namespaces, records, unions, fields, and attributes — far simpler to work with than the full FCS AST.

---

### ✨ What Djinn provides

- Fast, dependency‑light F# AST parsing  
- A clean, stable type model for:
  - records  
  - discriminated unions  
  - modules and namespaces  
  - fields and union cases  
  - attributes and constructor/named arguments (including `typeof<T>`)  
- Entry point detection via a custom marker attribute  
- Helpers for generating F# code from conventions  
- Analyzer‑friendly packaging (ships as a Roslyn analyzer)

Djinn does **not** perform type checking or symbol resolution. It stays purely syntactic so generators can layer their own semantics on top.

---

### 📦 Installation

```xml
<PackageReference Include="FSharp.SourceDjinn" Version="0.1.4" PrivateAssets="all" />
```

Djinn is intended for analyzers and source generators, not runtime use.

---

### 🧪 Example: scanning for a custom attribute

This example shows the core use case: extracting a simplified type model and finding types marked with your generator’s attribute.

```fsharp
open FSharp.SourceDjinn

let source = """
module App

[<Generate>]
type Person = { Name: string; Age: int }
"""

let types = TypeModel.extract "/test.fs" source

for t in types do
    if t.HasAttribute "Generate" then
        printfn "Found type: %s" t.FullName
        for f in t.Fields do
            printfn "  Field: %s : %s" f.Name f.TypeName
```

**Output:**

```
Found type: App.Person
  Field: Name : string
  Field: Age : int
```

This is the heart of Djinn: turn F# source into a clean, usable model for your generator.

---

### ⚡ Custom `EntryPoint` attribute

F# has a unique rule: the real `[<EntryPoint>]` function must appear **last in the compilation order**. Source generators run **before** compilation and cannot control file ordering, so they cannot safely generate a real entry point.

To solve this, Djinn provides a lightweight marker attribute:

```fsharp
[<FSharp.SourceDjinn.EntryPoint>]
```

This attribute is **not** the real entry point. Instead, it tells your generator:

> “This is the function the generator should wrap in the actual `[<EntryPoint>]` function it emits.”

Djinn’s `EntryPointDetector` picks up this attribute during extraction so generators can reliably locate the user’s intended entry point.

Generators typically:

- scan for `[<FSharp.SourceDjinn.EntryPoint>]`
- generate a real `[<EntryPoint>]` function in a separate file
- call the user’s function from that generated entry point

This keeps user code clean while ensuring the generated entry point appears in the correct place in the compilation order.

---

### 🧩 Architecture overview

Djinn is composed of small, focused components:

- **AstParser** — parses F# source into a simplified AST  
- **TypeModel** — extracts types, fields, union cases, attributes, and module paths  
- **EntryPointDetector** — finds top‑level entry points  
- **Emitter** — helps generate F# code using conventions  

These components are intentionally minimal so higher‑level libraries can layer semantics and code generation on top.

---

### 🌱 Ecosystem

Projects built on Djinn:

- **Serde.FS** — idiomatic F# serialization  
- **Serde.FS.SourceGen** — compile‑time serializer generator  
- **Serde.FS.Json** — System.Text.Json backend  

All projects live under the **fs‑djinn** GitHub organization.

---

### ❤️ Acknowledgments
A special hat tip to Dave Thomas and his work on [Myriad](https://github.com/MoiraeSoftware/myriad). Myriad was one of the first projects to show how flexible and expressive F# metaprogramming could be — taking arbitrary inputs (including F# code) and generating idiomatic F# constructs such as records and discriminated unions, all through a plugin‑based code‑generation flow. 
SourceDjinn follows in that lineage, focusing on attribute‑driven generation while carrying forward the spirit of making compile‑time F# tooling more accessible.

---

### 📄 License  
MIT

---
