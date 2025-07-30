const books = [
  { title: "Book1", Genre: "history", Publish: 1990,},
  { title: "Book1", Genre: "history", Publish: 1990 },
  { title: "Book1", Genre: "Science", Publish: 1990 },
  { title: "Book1", Genre: "Fiction", Publish: 1990 },
];

const userbooks = books.filter((bk) => bk.Genre==="history")

console.log(userbooks);

