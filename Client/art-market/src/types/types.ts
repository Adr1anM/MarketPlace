
export interface Author{
    id: number;
    userId: number;
    biography: string;
    country: string;
    birthDate: string;
    socialMediaLinks : string;
    numberOfPosts: number;
    profileImage: File | string | null; 
    firstName: string;
    lastName: string;
    email: string;
}

export interface Artwork{
    id: number;
    title: string;
    description: string;
    categoryID: number;
    authorId: number;
    quantity: number;
    price: number;
    createdDate: string;
    userId: number;
    firstName: string;
    lastName: string;
    imageData: File | string | null;
    subCategoryIds : number[];
}

export interface CreateArtwork{
    title: string;
    description: string;
    categoryID: number;
    authorId: number;
    quantity: number;
    price: number;
    createdDate: string;
    imageData: File | string | null;
    subCategoryIds : number[];
}

export interface Order{
    id: number;
    categoryId: number;
    promocodeId: number;
    quantity: number;
    statusId: number;
    totalPrice: number;
    createdDate: string;
}

export interface Category{
    id: number;
    name: string;
}

export interface SubCategory{
    id: number;
    name: string;
}


export interface CategoryWithSubcategories {
    categoryId: number;
    categoryName: string;
    subCategories: SubCategory[];
};

export interface AuthorCategory{
    id: number;
    name: string;
}

export interface Promocode{
    id: number;
    code: string;
    createdDate: string;
    expiereDate: string;
}


export interface User{
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    roles: string [];
}

export interface PagedRequest {
    pageIndex: number;
    pageSize: number;
    columnNameForSorting: string;
    sortDirection: 'asc' | 'desc';
    requestFilters: RequestFilters ;
}

export interface RequestFilters{
    logicalOperator: number;
    filters: Filter[] ;
}

export interface Filter {
    path: string;
    value: string ;
    operator: string;
}

export interface AuthorNames{
    firstName: string;
    lastName: string;
}

export interface PriceRange{
    min: number;
    max: number;
}

export interface DateRange {
    start: string | null;
    end: string | null;
}
  
export interface SelectedOptions {
    authors:  AuthorNames[];
    priceRange: PriceRange ;
    categories: string[];
    countries: string[];
    dateRange: DateRange | null;
}

export interface AddToCartModel{
    userId: number | undefined;
    productId: number;
    quantity: number;
}

export interface ShoppingCart{
    userId: number;
    createdDate: string;
    updatedDate: string;
    shoppingCartItems: ShoppingCartItems[]
};

export interface ShoppingCartItems{
    productId: number;
    title: string;
    productDescription: string;
    price: number;
    quantity: number;
};

export interface RegisterData {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    role: string;
}


export interface AuthorData {
    userId: number;
    biography: string;
    country: string;
    birthDate: string;
    socialMediaLinks: string;
    numberOfPosts: number;
    phoneNumber: string;
    profileImage: string | File | null;
}
  
export interface RegisterCommand {
    registerData: RegisterData;
    authorData?: AuthorData; 
}