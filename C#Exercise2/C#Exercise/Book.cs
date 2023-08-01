namespace LibraryManagementSystem
{
    public class Book
    {
        public int BookId { get; }
        public string Title { get; }
        public string Author { get; }

        public Book(int bookId, string title, string author)
        {
            BookId = bookId;
            Title = title;
            Author = author;
        }
    }
}
