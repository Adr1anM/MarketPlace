import { create } from "zustand";
import { Category } from "../../types/types";
import axios from "../../configurations/axios/axiosConfig";


// State type
type TCategoriesState = {
    categories: Category[];
    loading: boolean;
  };
  
  // Actions type
  type TCategoriesActions = {
    fetchCategories: () => void;
    
  };
  
  // Getter type
  type TCategoriesGetter = {
    getCategories: () => Category[];
    getCategoryById: (id: number) => Category | null;
  };
  
  const INITIAL_CATEGORIES_STATE: TCategoriesState = {
    categories: [],
    loading: false,
  };


  const useCategories = create<TCategoriesState & TCategoriesActions & TCategoriesGetter>((set, get) => ({
    ...INITIAL_CATEGORIES_STATE,
  
    fetchCategories: async () => {
      try {
        set((state) => ({ ...state, loading: true }));
        const response = await axios.get('/Product/categories'); 
        set((state) => ({ ...state, categories: response.data }));
      } catch (error) {
        console.error(error);
      } finally {
        set((state) => ({ ...state, loading: false }));
      }
    },
  
    getCategories: () => {
      return get().categories;
    },
  
    getCategoryById: (id: number) => {
      return get().categories.find((category) => category.id === id) || null;
    },
  }));
  
  export default useCategories;
  