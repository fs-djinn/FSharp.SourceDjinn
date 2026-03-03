namespace FSharp.SourceDjinn

module EntryPointEmitter =

    let emit (info: EntryPointInfo) : string =
        "namespace FSharp.SourceDjinn.Generated\n" +
        "\n" +
        "module internal DjinnBootstrap =\n" +
        "    let mutable private conventionBootstrapWasCalled = false\n" +
        "\n" +
        "    let tryConventionBootstrap () =\n" +
        "        try\n" +
        "            // Ensure referenced assemblies are loaded\n" +
        "            let entry = System.Reflection.Assembly.GetEntryAssembly()\n" +
        "            if not (isNull entry) then\n" +
        "                for name in entry.GetReferencedAssemblies() do\n" +
        "                    try System.Reflection.Assembly.Load(name) |> ignore with _ -> ()\n" +
        "\n" +
        "            // Search all loaded assemblies for the convention bootstrap\n" +
        "            for asm in System.AppDomain.CurrentDomain.GetAssemblies() do\n" +
        "                match asm.GetType(\"" + Conventions.ConventionBootstrapType + "\") with\n" +
        "                | null -> ()\n" +
        "                | ty ->\n" +
        "                    let m =\n" +
        "                        ty.GetMethod(\n" +
        "                            \"" + Conventions.ConventionBootstrapMethod + "\",\n" +
        "                            System.Reflection.BindingFlags.Public\n" +
        "                            ||| System.Reflection.BindingFlags.Static)\n" +
        "                    if not (isNull m) && m.GetParameters().Length = 0 then\n" +
        "                        m.Invoke(null, [||]) |> ignore\n" +
        "                        conventionBootstrapWasCalled <- true\n" +
        "        with _ -> ()\n" +
        "\n" +
        "    let fallbackToReflectionBootstrap () =\n" +
        "        if conventionBootstrapWasCalled then ()\n" +
        "        else\n" +
        "            // Minimal stub — no Djinn-owned metadata to activate yet.\n" +
        "            // Can be extended later for Djinn-scoped registrations.\n" +
        "            ()\n" +
        "\n" +
        sprintf "module DjinnEntryPoint =\n\n    [<EntryPoint>]\n    let main argv =\n        DjinnBootstrap.tryConventionBootstrap ()\n        DjinnBootstrap.fallbackToReflectionBootstrap ()\n        %s.%s argv\n"
            info.ModuleName info.FunctionName
