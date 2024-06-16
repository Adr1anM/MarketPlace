import { create } from "zustand";
import { Category, SubCategory } from "../../types/types";
import axios from "../../configurations/axios/axiosConfig";


// State type
type TSubCategoriesState = {
    categories: Category[];
    loading: boolean;
  };
  
  // Actions type
  type TSubCategoriesActions = {
    fetchCategories: () => void;
    
  };
  
  // Getter type
  type TSubCategoriesGetter = {
    getCategories: () => SubCategory[];
    getCategoryById: (id: number) => Category | null;
  };
  
  const INITIAL_CATEGORIES_STATE: TSubCategoriesState = {
    categories: [],
    loading: false,
  };


  const useSubCategories = create<TSubCategoriesState & TSubCategoriesActions & TSubCategoriesGetter>((set, get) => ({
    ...INITIAL_CATEGORIES_STATE,
  
    fetchCategories: async () => {
      try {
        set((state) => ({ ...state, loading: true }));
        const response = await axios.get('/Product/subcategories'); 
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
  
  export default useSubCategories;
  