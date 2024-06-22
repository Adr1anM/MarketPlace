import { create } from "zustand";
import axios from "../../configurations/axios/axiosConfig";
import { AddToCartModel } from "../../types/types";

type TShoppingCartItem = {
  productId: number;
  title: string;
  productDescription: string;
  price: number;
  quantity: number;
};

type TShoppingCart = {
  userId: number;
  createdDate: string;
  updatedDate: string;
  shoppingCartItems: TShoppingCartItem[];
  loading: boolean;
};

type TShoppingCartActions = {
  addToCart: (item: AddToCartModel) => Promise<void>;
  removeFromCart: (userId: number, productId: number) => Promise<void>;
  updateCartItem: (updatedItem: AddToCartModel) => Promise<void>;
  fetchCart: (userId: number | undefined) => Promise<void>;
  clearCart: () => void;
};

type TShoppingCartGetter = {
  getCart: () => TShoppingCart;
  getCartItems: () => TShoppingCartItem[];
  getCartItemById: (productId: number) => TShoppingCartItem | null;
  getCartItemCount: () => number;
};

const INITIAL_STATE: TShoppingCart = {
  userId: 0,
  createdDate: '',
  updatedDate: '',
  shoppingCartItems: [],
  loading: false
};

const useShoppingCart = create<TShoppingCart & TShoppingCartActions & TShoppingCartGetter>((set, get) => ({
  ...INITIAL_STATE,

  getCart: () => get(),

  getCartItems: () => get().shoppingCartItems,

  getCartItemById: (productId) => get().shoppingCartItems.find((item) => item.productId === productId) || null,

  getCartItemCount: () => get().shoppingCartItems.reduce((count, item) => count + item.quantity, 0),

  addToCart: async (item) => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.post('/ShoppingCarts', item);
      set((state) => ({
        ...response.data,
        loading: false,
      }));
    } catch (error) {
      console.error('Error adding to cart:', error);
      set((state) => ({ ...state, loading: false }));
    }
  },

  removeFromCart: async (userId, productId) => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.delete(`/ShoppingCarts?userId=${userId}&productId=${productId}`);
      set((state) => ({
        ...response.data,
        loading: false,
      }));
    } catch (error) {
      console.error('Error removing from cart:', error);
      set((state) => ({ ...state, loading: false }));
    }
  },

  updateCartItem: async (updatedItem) => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.put('/ShoppingCarts', updatedItem);
      set((state) => ({
        ...response.data,
        loading: false,
      }));
    } catch (error) {
      console.error('Error updating cart item:', error);
      set((state) => ({ ...state, loading: false }));
    }
  },

  fetchCart: async (userId) => {
    try {
      set((state) => ({ ...state, loading: true }));
      const response = await axios.get(`/ShoppingCarts/${userId}`);
      set((state) => ({
        ...response.data,
        loading: false,
      }));
    } catch (error) {
      console.error('Error fetching cart:', error);
      set((state) => ({ ...state, loading: false }));
    }
  },

  clearCart: () => {
    set(INITIAL_STATE);
  },
}));

export default useShoppingCart;