namespace LibraryManagementSystem
{
    class Program
    {
        static List<Member> members = new List<Member>();
        static List<Book> books = new List<Book>();

        static string membersFilePath = "members.csv";
        static string booksFilePath = "books.csv";


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Victor's Library Management System!");


            books.Add(new Book(1, "How to kill two birds with one stone", "Adekunle"));
            books.Add(new Book(2, "Mastering Javascript", "Demola Ogunyemi"));
            books.Add(new Book(3, "Best in coding C#", "Victor Ezike"));

            LoadMembersData();
            LoadBooksData();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Enter an option:");
                Console.WriteLine("1. Register as a member");
                Console.WriteLine("2. Browse books");
                Console.WriteLine("3. Borrow a book");
                Console.WriteLine("4. Return a book");
                Console.WriteLine("5. View lending history");
                Console.WriteLine("6. Exit");

                int option = UserInput(1, 6);

                switch (option)
                {
                    case 1:
                        RegisterMember();
                        break;
                    case 2:
                        BrowseBooks();
                        break;
                    case 3:
                        BorrowBook();
                        break;
                    case 4:
                        ReturnBook();
                        break;
                    case 5:
                        LendingHistory();
                        break;
                    case 6:
                        exit = true;
                        Console.WriteLine("Come back next time!");
                        break;
                }
            }
        }

        static void LoadMembersData()
        {
            if (File.Exists(membersFilePath))
            {
                string[] lines = File.ReadAllLines(membersFilePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int memberId))
                    {
                        members.Add(new Member(memberId, parts[1]));
                    }
                }
            }
        }

        static void LoadBooksData()
        {
            if (File.Exists(booksFilePath))
            {
                string[] lines = File.ReadAllLines(booksFilePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int bookId))
                    {
                        books.Add(new Book(bookId, parts[1], parts[2]));
                    }
                }
            }
        }

        static void SaveMembersData()
        {
            List<string> lines = new List<string>();
            foreach (var member in members)
            {
                lines.Add($"{member.MemberId},{member.Name}");
            }
            File.WriteAllLines(membersFilePath, lines);
        }

        static void SaveBooksData()
        {
            List<string> lines = new List<string>();
            foreach (var book in books)
            {
                lines.Add($"{book.BookId},{book.Title},{book.Author}");
            }
            File.WriteAllLines(booksFilePath, lines);
        }

        static void RegisterMember()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            int memberId = members.Count + 1;

            members.Add(new Member(memberId, name));

            Console.WriteLine("Registration successful! Your ID number is: " + memberId);
        }

        static void BrowseBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("There are no books for now. Check again later");
                return;
            }

            Console.WriteLine("Available books:");
            foreach (var book in books)
            {
                Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}");
            }
        }

        static void BorrowBook()
        {
            Console.Write("Enter your member ID: ");
            int memberId = UserInput(1, members.Count);

            Console.Write("Enter the book ID you want to borrow: ");
            int bookId = UserInput(1, books.Count);

            Member member = members.Find(m => m.MemberId == memberId);
            Book book = books.Find(b => b.BookId == bookId);

            if (member == null || book == null)
            {
                Console.WriteLine("Invalid member ID or book ID.");
                return;
            }

            member.LendingHistory.Add(book);

            Console.WriteLine($"Book '{book.Title}' successfully borrowed by {member.Name}.");
        }

        static void ReturnBook()
        {
            Console.Write("Enter your member ID: ");
            int memberId = UserInput(1, members.Count);

            Console.Write("Enter the book ID you want to return: ");
            int bookId = UserInput(1, books.Count);

            Member member = members.Find(m => m.MemberId == memberId);
            Book book = books.Find(b => b.BookId == bookId);

            if (member == null || book == null)
            {
                Console.WriteLine("Invalid member ID or book ID.");
                return;
            }

            if (!member.LendingHistory.Contains(book))
            {
                Console.WriteLine("You have not borrowed this book.");
                return;
            }

            member.LendingHistory.Remove(book);

            Console.WriteLine($"Book '{book.Title}' successfully returned by {member.Name}.");
        }

        static void LendingHistory()
        {
            Console.Write("Enter your member ID: ");
            int memberId = UserInput(1, members.Count);

            Member member = members.Find(m => m.MemberId == memberId);

            if (member == null)
            {
                Console.WriteLine("Invalid member ID.");
                return;
            }

            if (member.LendingHistory.Count == 0)
            {
                Console.WriteLine("You have not borrowed any books.");
                return;
            }

            Console.WriteLine($"Lending history for {member.Name}:");
            foreach (var book in member.LendingHistory)
            {
                Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}");
            }
        }

        static int UserInput(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.Write($"Invalid input. Please enter a number between {min} and {max}: ");
            }
            return input;
        }
    }
}
