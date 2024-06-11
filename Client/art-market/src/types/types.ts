
export interface Author{
    id: number;
    userId: number;
    biography: string;
    country: string;
    birthDate: string;
    socialMediaLinks : string;
    numberOfPosts: number;
}

export interface Artwork{
    id: number;
    title: string;
    description: string;
    categoryId: number;
    authorId: number;
    quantity: number;
    price: number;
    createdDate: string;
    userId: number,
    firstName: string,
    lastName: string
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
}