"use client";
import React, { ReactNode } from "react";
import QueryProvider from "./QueryProvider";
import AuthProvider from "./AuthProvider";
import { GoogleOAuthProvider } from "@react-oauth/google";

const Providers = ({ children }: { children: ReactNode }) => {
  const clientId = process.env.NEXT_PUBLIC_GOOGLE_ID || "";

  return (
    <GoogleOAuthProvider clientId={clientId}>
      <QueryProvider>
        <AuthProvider>{children}</AuthProvider>
      </QueryProvider>
    </GoogleOAuthProvider>
  );
};

export default Providers;
