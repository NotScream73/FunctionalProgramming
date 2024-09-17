// Лабораторная работа 2 - Последовательности
// Для выполнения работы потребуется текстовый файл мегабайт на 5 – 10 для создания последовательности.
// Для текстового файла определить количество каждой из согласных
open System
open System.IO
open System.Text

let generateFile (filePath: string) =

    let russianAlphabet = 
        "АБВГДЕЁЖЗИИЙКЛМНОПРСТУФХЦЧШЩЬЫЭЮЯ" +
        "абвгдеёжзийклмнопрстуфхцчшщьыэюя"

    let encoding = Encoding.UTF8

    let random = Random()

    use fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)
    use streamWriter = new StreamWriter(fileStream, encoding)

    let mutable totalBytesWritten = 0
    while totalBytesWritten < 3932160 do
        russianAlphabet[random.Next(0, russianAlphabet.Length)] |> streamWriter.Write
        totalBytesWritten <- totalBytesWritten + 1

generateFile "russian_text_file.txt"

let consonants = 
    "БВГДЖЗКЛМНПРСТФХЦЧШЩ" + "бвгджзклмнпрстфхцчшщ"

let countConsonants (filePath: string) =
    let content = File.ReadAllText(filePath, Encoding.UTF8)
    
    let countFrequency (text: string) =
        text
        |> Seq.filter (fun c -> consonants.Contains(c))
        |> Seq.countBy id
        |> Seq.sortBy fst
    
    let consonantCounts = countFrequency content
    
    printfn "Количество каждой согласной:"
    for (consonant, count) in consonantCounts do
        printfn "%c: %d" consonant count

countConsonants "russian_text_file.txt"