### ✨ FSharp.SourceDjinn  
A lightweight, intention‑revealing **F# source generation engine**. Djinn provides the building blocks for analyzers and code generators: AST parsing, type modeling, entry point detection, and a clean convention‑driven pipeline for emitting F# code.

Djinn powers the **Serde.FS** ecosystem, but it is fully independent and can be used by any F# source generator.

---

### 🚀 Features

- Fast, dependency‑light F# AST parsing  
- Type model extraction for records, unions, modules, and namespaces  
- Entry point detection via `[<EntryPoint>]` or `FSharp.SourceDjinn.EntryPoint`  
- Convention‑driven code emission  
- Analyzer‑friendly packaging (ships as a Roslyn analyzer)

---

### 📦 Installing

```xml
<PackageReference Include="FSharp.SourceDjinn" Version="0.1.0" PrivateAssets="all" />
```

Djinn is intended for use inside analyzers and source generators, not as a runtime dependency.

---

### 🧩 Basic Usage

```fsharp
open FSharp.SourceDjinn

let source = """
module App

[<EntryPoint>]
let run argv = 0
"""

let entry = EntryPointDetector.detect "/test.fs" source

match entry with
| Some ep -> printfn "Entry point: %s.%s" ep.ModuleName ep.FunctionName
| None -> printfn "No entry point found"
```

---

### 🧱 Architecture

Djinn is built around three core components:

- **AstParser** — parses F# source into a simplified AST  
- **TypeModel** — extracts semantic information about types  
- **EntryPointDetector** — finds top‑level entry points  
- **Emitter** — helps generate F# code from conventions  

These components are intentionally small and composable so that higher‑level libraries (like Serde.FS) can build on top of them.

---

### 🧪 Status

Djinn is early but stable enough for real use. The API may evolve as the ecosystem grows.

---

### 📚 Ecosystem

- **Serde.FS** — F# serialization library powered by Djinn  
- **Serde.FS.SourceGen** — Serde compiler plugin  
- **Serde.FS.SystemTextJson** — JSON backend  

All projects live under the **fs‑djinn** GitHub organization.

---

### 📄 License

MIT

---
