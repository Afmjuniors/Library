import { useState, useEffect } from 'react';
import bookService from '../services/bookService';
import { Book, SearchBookParams } from '../types';

export const useBooks = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchBooks = async (params?: SearchBookParams) => {
    try {
      setLoading(true);
      setError(null);
      const fetchedBooks = await bookService.getBooks(params);
      setBooks(fetchedBooks);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to fetch books');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const createBook = async (bookData: any) => {
    try {
      setError(null);
      const newBook = await bookService.createBook(bookData);
      setBooks(prev => [...prev, newBook]);
      return newBook;
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to create book');
      throw err;
    }
  };

  const updateBook = async (bookData: any) => {
    try {
      setError(null);
      const updatedBook = await bookService.updateBook(bookData);
      setBooks(prev => prev.map(book => 
        book.bookId === updatedBook.bookId ? updatedBook : book
      ));
      return updatedBook;
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to update book');
      throw err;
    }
  };

  const deleteBook = async (bookId: number) => {
    try {
      setError(null);
      await bookService.deleteBook(bookId);
      setBooks(prev => prev.filter(book => book.bookId !== bookId));
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to delete book');
      throw err;
    }
  };

  const requestLoan = async (bookId: number) => {
    try {
      setError(null);
      await bookService.requestLoan(bookId);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to request loan');
      throw err;
    }
  };

  const returnBook = async (bookId: number) => {
    try {
      setError(null);
      await bookService.returnBook(bookId);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to return book');
      throw err;
    }
  };

  return {
    books,
    loading,
    error,
    fetchBooks,
    createBook,
    updateBook,
    deleteBook,
    requestLoan,
    returnBook,
  };
}; 