"use client";
import { userChatType } from "@/Types";
import { createContext, ReactNode, useEffect, useState } from "react";
import Cookie from "js-cookie";

type AuthProviderProps = {
  children: ReactNode;
};

type AuthContextProps = {
  userGlobalData: userChatType | null;
  setUserGlobalData: (user: userChatType | null) => void;
};

const defaultValueAuthContext: AuthContextProps = {
  userGlobalData: null,
  setUserGlobalData: () => {},
};

export const AuthContext = createContext<AuthContextProps>(
  defaultValueAuthContext
);

const AuthProvider = ({ children }: AuthProviderProps) => {
  const [userGlobalData, setUserGlobalData] = useState<userChatType | null>(
    null
  );

  useEffect(() => {
    const userData = Cookie.get("user");
    if (userData) {
      setUserGlobalData(JSON.parse(userData));
    }
  }, []);

  return (
    <AuthContext.Provider value={{ userGlobalData, setUserGlobalData }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
