import { create } from "zustand";
import { Category, SubCategory } from "../../types/types";
import axios from "../../configurations/axios/axiosConfig";


// State type
type TSubCategoriesState = {
  subCategories: Category[];
    loading: boolean;
  };
  
  // Actions type
  type TSubCategoriesActions = {
    fetchSubCategories: () => void;
    fetchSubCategoriesByCategId: (id:number) => void;
    
  };
  
  // Getter type
  type TSubCategoriesGetter = {
    getSubCategories: () => SubCategory[];
    getSubCategoryById: (id: number) => Category | null;
  };
  
  const INITIAL_CATEGORIES_STATE: TSubCategoriesState = {
    subCategories: [],
    loading: false,
  };


  const useSubCategories = create<TSubCategoriesState & TSubCategoriesActions & TSubCategoriesGetter>((set, get) => ({
    ...INITIAL_CATEGORIES_STATE,
  
    fetchSubCategories: async () => {
      try {
        set((state) => ({ ...state, loading: true }));
        const response = await axios.get('/LookUp/subcategories'); 
        set((state) => ({ ...state, subCategories: response.data }));
      } catch (error) {
        console.error(error);
      } finally {
        set((state) => ({ ...state, loading: false }));
      }
    },

    fetchSubCategoriesByCategId: async (id:number) => {
      try {
        set((state) => ({ ...state, loading: true }));
        const response = await axios.get(`/LookUp/subcategories/${id}`); 
        set((state) => ({ ...state, subCategories: response.data }));
      } catch (error) {
        console.error(error);
      } finally {
        set((state) => ({ ...state, loading: false }));
      }
    },
  
    getSubCategories: () => {
      return get().subCategories;
    },
  
    getSubCategoryById: (id: number) => {
      return get().subCategories.find((category) => category.id === id) || null;
    },
  }));
  
  export default useSubCategories;
  