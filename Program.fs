// Создать иерархию классов и интерфейсов для предметной области 
// (класса 3 – 4, из них хотя бы один класс должен являться 
// наследником другого класса), заданной номером варианта, и 
// продемонстрировать её работу
// Библиотека

type IBook =
    abstract Title: string
    abstract Author: string
    abstract GetInfo: unit -> string

type IPerson =
    abstract Name: string
    abstract BorrowBook: IBook -> unit
    abstract ReturnBook: IBook -> unit

type Book(title: string, author: string) =
    interface IBook with
        member this.Title = title
        member this.Author = author
        member this.GetInfo() = $"Книга: {title} от {author}"

type EBook(title: string, author: string, format: string) =
    inherit Book(title, author)
    member this.Format = format
    interface IBook with
        member this.GetInfo() = $"Электронная книга: {title} от {author} в формате {format}"

type User(name: string) =
    let mutable borrowedBooks = []
    interface IPerson with
        member this.Name = name
        member this.BorrowBook(book: IBook) =
            borrowedBooks <- book :: borrowedBooks
            printfn "%s взял книгу: %s" name (book.GetInfo())
        member this.ReturnBook(book: IBook) =
            borrowedBooks <- List.filter ((<>) book) borrowedBooks
            printfn "%s вернул книгу: %s" name (book.GetInfo())

type Librarian(name: string) =
    inherit User(name)
    member this.ManageBooks() =
        printfn "%s управляет книгами в библиотеке" name

let book = Book("Война и мир", "Лев Толстой") :> IBook

let ebook = EBook("Гарри Поттер", "Джоан Роулинг", "PDF") :> IBook

let user : IPerson = User("Иван")
let librarian = Librarian("Анна")

user.BorrowBook(book)
user.BorrowBook(ebook)

user.ReturnBook(book)

librarian.ManageBooks()