﻿namespace AST

open System 

type Program(programMembers : ProgramMember list, pos : Position) =
    inherit Node(pos)
    
    let mutable nameMainClass = "" 

    let classes    = List.fold (fun acc (m : ProgramMember) -> 
                                    try m :?> Class :: acc 
                                    with :? InvalidCastException -> acc
                               ) [] programMembers

    let interfaces = List.fold (fun acc (m : ProgramMember) -> 
                                    try m :?> Interface :: acc 
                                    with :? InvalidCastException -> acc
                               ) [] programMembers

    member x.ProgramMembers = programMembers
    member x.NameMainClass with get() = nameMainClass
                           and set(s) = nameMainClass <- s 
    member x.Classes        = classes
    member x.Interfaces     = interfaces

    override x.ToString() = programMembers
                            |> List.map string
                            |> String.concat "\n\n" 
