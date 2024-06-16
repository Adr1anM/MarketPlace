import {create} from "zustand";
import axios from "../../configurations/axios/axiosConfig";
import { Artwork, CreateArtwork,PagedRequest } from "../../types/types";
import { base64ToFile } from "./Converter";


// Initial state for Artwork
const INITIAL_ARTWORK_STATE: Artwork = {
  id: 0,
  title: '',
  description: '',
  categoryID: 0,
  authorId: 0,
  quantity: 0,
  price: 0,
  createdDate: '',
  userId: 0,
  firstName: '',
  lastName: '',
  imageData: '',
  subCategoryIds: []
};

// State type
type TArtwork = typeof INITIAL_ARTWORK_STATE;
type TArtworks = {
  artworks: TArtwork[];
  loading: boolean;
};

type TPagedArtworks = {
  artworks: Artwork[];
  loading: boolean;
  pageIndex: number;
  pageSize: number;
  total: number;
};


// Actions type
type TArtworksActions = {
  setInitialArtworks: (artworks: TArtwork[]) => void;
  deleteArtworkById: (id: number) => void;
  fetchArtworks: () => void;
  fetchPagedArtworks: (pegedRequest:PagedRequest) => void;
  fetchArtworksByAuthorId: (id: number) => void;
  fetchArtworkById: (id: number) => Promise<TArtwork | null>;
  createArtwork: (artwork: CreateArtwork) => Promise<void>;
  updateArtwork: (artwork: TArtwork) => Promise<void>;
};

// Getter type
type TArtworksGetter = {
  getArtworks: () => TArtwork[];
  getArtworkById: (id: number) => TArtwork | null;
};

// Initial state
const INITIAL_ARTWORKS_STATE: TArtworks & { pagedArtworks: TPagedArtworks | null } = {
  artworks: [] as TArtwork[],
  loading: false,
  pagedArtworks: null
};

// Create the store
const useArtworks = create<TArtworks & TArtworksActions & TArtworksGetter & { pagedArtworks: TPagedArtworks | null } >((set, get) => ({
  ...INITIAL_ARTWORKS_STATE,

  getArtworkById: (id) => {
    return get().artworks.find((artwork) => artwork.id === id) || null;
  },

  deleteArtworkById: async (id) => {
    try {
      await axios.delete(`/Product/${id}`);
      set((state) => ({
        ...state,
        artworks: state.artworks.filter((artwork) => artwork.id !== id),
      }));
    } catch (error) {
      console.error(error);
    }
  },
  
  fetchArtworks: async () => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.get('/Product/all');
      set((state) => ({ ...state, artworks: response.data }));
    } catch (error) {
      console.error(error);
    } finally {
      set((state) => ({ ...state, loading: false }));
    }
  },

  fetchPagedArtworks: async (pagedRequest: PagedRequest) =>  {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.post('/Product/paged', pagedRequest);
      const pagedResult = response.data;
      set((state) => ({ 
        ...state,
        pagedArtworks: {
          artworks: pagedResult.items,
          loading: false,
          pageIndex: pagedResult.pageIndex,
          pageSize: pagedResult.pageSize,
          total: pagedResult.total
        },
        loading: false
      }));
    } catch (error) {
      console.error(error);
      set((state) => ({ ...state, loading: false }));
    }
  },

  fetchArtworksByAuthorId: async (id: number) => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.get(`/Product/all/${id}`);
      set((state) => ({ ...state, artworks: response.data }));
    } catch (error) {
      console.error(error);
    } finally {
      set((state) => ({ ...state, loading: false }));
    }
  },

  fetchArtworkById: async (id) => {
    try {
      const response = await axios.get(`/Product/${id}`);
      return response.data as TArtwork;
    } catch (error) {
      console.error(error);
      return null;
    }
  },

  createArtwork: async (artwork) => {
    try {
      const formData = new FormData();
      formData.append('Title', artwork.title);
      formData.append('Description', artwork.description);
      formData.append('CategoryID', artwork.categoryID.toString());
      formData.append('AuthorId', artwork.authorId.toString());
      formData.append('Quantity', artwork.quantity.toString());
      formData.append('Price', artwork.price.toString());
      formData.append('CreatedDate', artwork.createdDate);
  
      artwork.subCategoryIds.forEach((id, index) => { 
        formData.append(`SubCategoryIds[${index}]`, id.toString());
      });
      if (typeof artwork.imageData === 'string') {
        const mimeType = 'image/jpeg'; 
        const file = base64ToFile(artwork.imageData, mimeType);
        formData.append('ImageData', file);
      } else if (artwork.imageData instanceof File) {
        formData.append('ImageData', artwork.imageData);
      }
      const response = await axios.post('/Product', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      set((state) => ({
        ...state,
        artworks: [...state.artworks, response.data],
      }));
    } catch (error) {
      console.error('Error creating artwork:', error);
      throw error;
    }
  },

  updateArtwork: async (artwork) => {
    try {
     
      const formData = new FormData();
      formData.append('Id', artwork.id.toString());
      formData.append('Title', artwork.title);
      formData.append('Description', artwork.description);
      console.log("enterd updateArtwork");
      formData.append('CategoryID', artwork.categoryID.toString());
      formData.append('AuthorId', artwork.authorId.toString());
      formData.append('Quantity', artwork.quantity.toString());
      formData.append('Price', artwork.price.toString());
      
      const dateObject = new Date(artwork.createdDate); 
      formData.append('CreatedDate', dateObject.toISOString());
      artwork.subCategoryIds.forEach((id, index) => {
        formData.append(`SubCategoryIds[${index}]`, id.toString());
      });
      
      if (typeof artwork.imageData === 'string' ) {
        const mimeType = 'image/jpeg'; 
        console.log('Base64 String:', artwork.imageData);
        const file = base64ToFile(artwork.imageData, mimeType); 
        formData.append('ImageData', file);
      } else if (artwork.imageData instanceof File) {
        console.log('it is a file:', artwork.imageData);
        formData.append('ImageData', artwork.imageData);
      }
      
      console.log("The object to be sent");
      console.log('FormData Contents:');
      for (const [key, value] of formData.entries()) {
          console.log(`${key}: ${value}`);
      } 
      await axios.put('/Product', formData, {
          headers: {
              'Content-Type': 'multipart/form-data',
          },
      });

      set((state) => ({ 
          ...state,
          artworks: state.artworks.map((a) => (a.id === artwork.id ? artwork : a)),
      }));

  } catch (error) {
      throw error;
  }
  },

  getArtworks: () => {
    return get().artworks;
  },

  setInitialArtworks: (artworks) => {
    set((state) => ({
      ...state,
      artworks,
    }));
  },
}));

export default useArtworks;