﻿open CLIArgs
open System
open System.IO
open FSharp.Data
open NBConvert.NET
open NBFormat.NET
open NBFormat.NET.Domain

[<EntryPoint>]
let main args =
    let parsedArgs = parser.ParseCommandLine args
    
    printfn "parsed args: %A" (parsedArgs.GetAllResults())

    let currentDir = System.Environment.CurrentDirectory

    let notebookPath = parsedArgs.GetResult InputNotebook

    let outputDir = 
         match parsedArgs.TryGetResult Output_Dir with
         | Some dir -> dir
         | None -> currentDir

    let toFormat = parsedArgs.GetResult To

    let parsedNotebook =
        notebookPath
        |> File.ReadAllText 
        |> Serialization.deserializeNotebook

    match toFormat with
    | OutputFormat.HTML ->
        let outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(notebookPath) + ".html")

        let convertedNotebook = 
            parsedNotebook
            |> NBConvert.NET.API.convert(HTMLConverter HTMLConverterTemplates.Default)

        File.WriteAllText(outputPath, convertedNotebook)
    | _ -> failwith "Invalid output format"
    0