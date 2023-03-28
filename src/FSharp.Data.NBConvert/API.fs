﻿namespace FSharp.Data.NBConvert

open FSharp.Data.NBFormat
open FSharp.Data.NBFormat.Domain
open FSharp.Data.NBConvert

open Giraffe.ViewEngine

type API() =

    static member convert (
        notebook: Notebook,
        converter: NotebookConverter
    ) =
        match converter with
        | HTMLConverter converter ->
            converter.ConvertNotebook notebook
            |> RenderView.AsString.htmlDocument