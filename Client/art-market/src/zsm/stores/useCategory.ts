import { create } from "zustand";
import { Category, CategoryWithSubcategories, SubCategory } from "../../types/types";
import axios from "../../configurations/axios/axiosConfig";




type TCategoriesState = {
  categories: CategoryWithSubcategories[];
  loading: boolean;
};
  

type TCategoriesActions = {
  fetchCategories: () => void;
  
};  
  

type TCategoriesGetter = {
  getCategories: () => CategoryWithSubcategories[];
  getCategoryById: (id: number) => CategoryWithSubcategories | null;
  getCategoryIdsAndNames: () => { id: number; name: string }[];
  getSubcategoriesForCategory: (categoryId: number | undefined) => { id: number; name: string }[];
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
      const response = await axios.get('/LookUp/categories'); 
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
    return get().categories.find((category) => category.categoryId  === id) || null;
  },

  getCategoryIdsAndNames: () => {
    const categories = get().categories;
    return categories.map(({ categoryId, categoryName }) => ({ id: categoryId, name: categoryName }));
  },

  getSubcategoriesForCategory: (categoryId: number | undefined) => {
    const category = get().categories.find((category) => category.categoryId === categoryId);
    if (!category) {
      return [];
    }
    return category.subCategories.map(({ id, name }) => ({ id, name }));
  },
}));

export default useCategories;
