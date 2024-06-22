import { User } from "../types/types";

export default interface AuthContextType{
    token: string | null;
    login: (token:string) => void;
    logout: () => void;
    isLoggedIn: boolean;
    user: User | undefined;
    setUser: React.Dispatch<React.SetStateAction<User | undefined >>;
}