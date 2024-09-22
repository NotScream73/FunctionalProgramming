// Задать интерпретацию команд и построить первые итерации
// в задаваемом количестве для следующей L-системы
// F++F++F++, F -> F-F++F-F

let applyRule symbol =
    match symbol with
    | 'F' -> "F-F++F-F"
    | '+' -> "+"
    | '-' -> "-"
    | _ -> string symbol

let rec applyRules (input: string) : string =
    input |> Seq.map applyRule |> String.concat ""

let rec iterateLSystem (axiom: string) (iterations: int) : string =
    if iterations = 0 then
        axiom
    else
        let newAxiom = applyRules axiom
        iterateLSystem newAxiom (iterations - 1)

[<EntryPoint>]
let main _ =
    let axiom = "F++F++F++"

    let iterations = 3

    let result = iterateLSystem axiom iterations
    printfn "Результат после %d итераций: %s" iterations result

    0
