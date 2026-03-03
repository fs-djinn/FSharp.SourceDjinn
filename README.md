### ✨ FSharp.SourceDjinn  
A lightweight, intention‑revealing **F# source generation engine**.  
Djinn extracts **syntactic metadata** from F# code — types, attributes, modules, entry points — and presents it through a clean, backend‑agnostic model that analyzers and code generators can build on.

Djinn powers the [**Serde.FS**](https://github.com/fs-djinn/Serde.FS) ecosystem, but it is fully independent and suitable for any F# source generator.

---

### 🚀 What Djinn provides
- Fast, dependency‑light F# AST parsing  
- Stable type model extraction for records, unions, modules, and namespaces  
- Attribute and constructor argument extraction (including `typeof<T>`)  
- Entry point detection via `[<EntryPoint>]` or `FSharp.SourceDjinn.EntryPoint`  
- Convention‑driven helpers for emitting F# code  
- Analyzer‑friendly packaging (ships as a Roslyn analyzer)

Djinn focuses on **syntax**, not semantics — it does not interpret types, resolve symbols, or perform type checking. This keeps it fast, predictable, and easy to embed.

---

### 📦 Installing

```xml
<PackageReference Include="FSharp.SourceDjinn" Version="0.1.4" PrivateAssets="all" />
```

Djinn is intended for use inside analyzers and source generators, not as a runtime dependency.

---

### 🧪 Basic example: extracting types

```fsharp
open FSharp.SourceDjinn

let source = """
module App

type Person = { Name: string; Age: int }
"""

let types = TypeModel.extract "/test.fs" source

for t in types do
    printfn "Found type: %s" t.Name
```

Output:

```
Found type: Person
```

---

### 🧩 Architecture overview

Djinn is built around four small, composable components:

- **AstParser** — parses F# source into a simplified AST  
- **TypeModel** — extracts records, unions, modules, namespaces, and attributes  
- **EntryPointDetector** — finds top‑level entry points  
- **Emitter** — helps generate F# code from conventions  

These components are intentionally minimal so higher‑level libraries (like Serde.FS) can layer semantics and code generation on top.

---

### 📚 Ecosystem

Projects built on Djinn:

- **Serde.FS** — idiomatic F# serialization  
- **Serde.FS.SourceGen** — compile‑time serializer generator  
- **Serde.FS.Json** — System.Text.Json backend  

All projects live under the **fs‑djinn** GitHub organization.

---

### 📄 License  
MIT

---
