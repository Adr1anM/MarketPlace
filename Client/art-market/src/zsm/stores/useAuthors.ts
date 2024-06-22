import { create } from "zustand";
import axios from "../../configurations/axios/axiosConfig";
import { Author, AuthorData, AuthorNames } from "../../types/types";
import { base64ToFile } from "./Converter";


const INITIAL_AUTHOR_STATE: Author = {
  id: 0,
  userId: 0,
  biography: '',
  country: '',
  birthDate: '',
  socialMediaLinks: '',
  numberOfPosts: 0,
  profileImage: '',
  firstName: '',
  lastName: '',
  email: ''
};


const INITIAL_CREATE_AUTHOR_STATE: AuthorData ={
  userId: 0,
  biography: '',
  country: '',
  birthDate: '',
  socialMediaLinks: '',
  numberOfPosts: 0,
  phoneNumber: '',
  profileImage:  '',
} 

// State type for authors
type TAuthor = typeof INITIAL_AUTHOR_STATE;
type TCreateAuthor = typeof INITIAL_CREATE_AUTHOR_STATE;
type TAuthors = {
  authors: TAuthor[];
  loading: boolean;
};

// Actions type for authors
type TAuthorsActions = {
  setInitialAuthors: (authors: TAuthor[]) => void;
  deleteAuthorById: (id: number) => void;
  fetchAuthors: () => void;
  fetchAllCountries: () => Promise<string[] | null>;
  fetchAllAuthorNames: () => Promise<AuthorNames[] | null>;
  fetchAuthorById: (id: number) => Promise<TAuthor | null>;
  fetchAuthorByUserId: (id:number) => Promise<TAuthor | null>;
  createAuthor: (author: TCreateAuthor) => Promise<void>;
  updateAuthor: (author: TAuthor) => Promise<void>;
};

// Getter type for authors
type TAuthorsGetter = {
  getAuthors: () => TAuthor[];
  getAuthorById: (id: number) => TAuthor | null;
  getAuthorByUserId: (id:number) => TAuthor | null;
};


const INITIAL_AUTHORS_STATE: TAuthors = {
  authors: [] as TAuthor[],
  loading: false,
};


const useAuthors = create<TAuthors & TAuthorsActions & TAuthorsGetter>((set, get) => ({
  ...INITIAL_AUTHORS_STATE,

  getAuthorById: (id) => {
    return get().authors.find((author) => author.id === id) || null;
  },

  getAuthorByUserId: (id) => {
    return get().authors.find((author) => author.userId === id) || null;
  },

  deleteAuthorById: async (id) => {
    try {
      await axios.delete(`/Authors/${id}`);
      set((state) => ({
        ...state,
        authors: state.authors.filter((author) => author.id !== id),
      }));
    } catch (error) {
      console.error(error);
    }
  },

  fetchAuthors: async () => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.get('/Authors/all');
      set((state) => ({ ...state, authors: response.data }));
    } catch (error) {
      console.error(error);
    } finally {
      set((state) => ({ ...state, loading: false }));
    }
  },

  fetchAuthorById: async (id) => {
    try {
      const response = await axios.get(`/Authors/${id}`);
      return response.data as TAuthor;
    } catch (error) {
      console.error(error);
      return null;
    }
  },

  fetchAllCountries: async () => {
    try {
      const response = await axios.get('/Authors/countries');
      return response.data as string[];
    } catch (error) {
      console.error(error);
      return null;
    }
  },

  fetchAllAuthorNames: async () => {
    try {
      const response = await axios.get('/Authors/names');
      return response.data as AuthorNames[];
    } catch (error) {
      console.error(error);
      return null;
    }
  },

  fetchAuthorByUserId: async (id) => {
    try {
      const response = await axios.get(`/Authors/by-user/${id}`);
      return response.data as TAuthor;
    } catch (error) {
      console.error(error);
      return null;
    }
  },

  createAuthor: async (author) => {
    try {
      const formData = new FormData();
      formData.append('UserId', author.userId.toString());
      formData.append('Biography', author.biography);
      formData.append('Country', author.country);
      const dateObject = new Date(author.birthDate); 
      formData.append('BirthDate', dateObject.toISOString());
      formData.append('SocialMediaLinks', author.socialMediaLinks);
      formData.append('NumberOfPosts', author.numberOfPosts.toString());
      formData.append('PhoneNumber', author.phoneNumber);

      
      if (typeof author.profileImage === 'string' ) {
        const mimeType = 'image/jpeg'; 
        console.log('Base64 String:', author.profileImage);
        const file = base64ToFile(author.profileImage, mimeType); 
        formData.append('ProfileImage', file);
      } else if (author.profileImage instanceof File) {
        console.log('it is a file:', author.profileImage);
        formData.append('ProfileImage', author.profileImage);
      }
      
      console.log("The object to be sent");
      console.log('FormData Contents:');
      for (const [key, value] of formData.entries()) {
          console.log(`${key}: ${value}`);
      } 
      const response = await axios.post('/Authors', formData, {
          headers: {
              'Content-Type': 'multipart/form-data',
          },
      });

      set((state) => ({
        ...state,
        authors: [...state.authors, response.data],
      }));
    } catch (error) {
      console.error('Error creating author:', error);
      throw error;
    }
  },

  updateAuthor: async (author) => {
    try {
        const formData = new FormData();
        formData.append('Id', author.id.toString());
        formData.append('UserId', author.userId.toString());
        formData.append('Biography', author.biography);
        formData.append('Country', author.country);
        const dateObject = new Date(author.birthDate); 
        formData.append('BirthDate', dateObject.toISOString());
        formData.append('SocialMediaLinks', author.socialMediaLinks);
        formData.append('NumberOfPosts', author.numberOfPosts.toString());
        
        if (typeof author.profileImage === 'string' ) {
          const mimeType = 'image/jpeg'; 
          console.log('Base64 String:', author.profileImage);
          const file = base64ToFile(author.profileImage, mimeType); 
          formData.append('ProfileImage', file);
        } else if (author.profileImage instanceof File) {
          console.log('it is a file:', author.profileImage);
          formData.append('ProfileImage', author.profileImage);
        }
        
        console.log("The object to be sent");
        console.log('FormData Contents:');
        for (const [key, value] of formData.entries()) {
            console.log(`${key}: ${value}`);
        } 
        await axios.put('/Authors', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });

        set((state) => ({ 
            ...state,
            authors: state.authors.map((a) => (a.id === author.id ? author : a)),
        }));
    } catch (error) {
        console.error('Error updating author:', error);
        throw error;
    }
},

  getAuthors: () => {
    return get().authors;
  },

  setInitialAuthors: (authors) => {
    set((state) => ({
      ...state,
      authors,
    }));
  },
}));

export default useAuthors;


