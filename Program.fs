// Для массива текстовых файлов выполнить задание в парадигме 
// параллельного программирования и сформировать единый 
// выходной файл.
// Для текстового файла определить количество каждой из согласных
open System.IO

// Функция для проверки, является ли символ согласной
let isConsonant c =
    match System.Char.ToLower(c) with
    | 'b' | 'c' | 'd' | 'f' | 'g' | 'h' | 'j' | 'k' | 'l' | 'm' 
    | 'n' | 'p' | 'q' | 'r' | 's' | 't' | 'v' | 'w' | 'x' | 'z' -> true
    | _ -> false

// Функция для подсчета согласных в одном файле
let countConsonantsInFile (filePath: string) =
    async {
        let! text = File.ReadAllTextAsync(filePath) |> Async.AwaitTask
        let consonantCount = 
            text.ToCharArray()
            |> Array.filter isConsonant
            |> Array.groupBy id
            |> Array.map (fun (c, arr) -> (c, Array.length arr))
        return consonantCount
    }

// Параллельная обработка нескольких файлов и объединение результатов
let processFilesInParallel (filePaths: string[]) =
    let results = 
        filePaths
        |> Array.map countConsonantsInFile
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Array.concat  // Объединяем все результаты в один массив

    // Объединяем одинаковые символы и суммируем их количество
    results
    |> Array.groupBy fst
    |> Array.map (fun (c, counts) -> (c, counts |> Array.sumBy snd))

// Функция для записи результатов в файл
let writeResultsToFile (outputPath: string) (consonantCounts: (char * int)[]) =
    let formattedResult = 
        consonantCounts 
        |> Array.map (fun (c, count) -> sprintf "%c: %d" c count)
        |> String.concat "\n"
    File.WriteAllText(outputPath, formattedResult)

let processAndSaveResults (inputFiles: string[]) (outputFile: string) =
    let totalConsonantCounts = processFilesInParallel inputFiles
    writeResultsToFile outputFile totalConsonantCounts

// Пример использования
let inputFiles = [| "file1.txt"; "file2.txt"; "file3.txt" |]
let outputFile = "output.txt"

processAndSaveResults inputFiles outputFile
