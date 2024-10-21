import { NextRequest, NextResponse } from "next/server";

export function middleware(request: NextRequest) {
  const user = request.cookies.get("user")?.value;

  const authUrl = new URL("/auth", request.url);
  const homeUrl = new URL("/", request.url);

  if (!user) {
    if (request.nextUrl.pathname === "/auth") {
      return NextResponse.next();
    }

    return NextResponse.redirect(authUrl);
  }

  if (request.nextUrl.pathname === "/auth") {
    return NextResponse.redirect(homeUrl);
  }
}

// Aplica o middleware apenas às rotas protegidas
export const config = {
  matcher: [
    "/profile/:path*",
    "/chat/:path*",
    "/auth",
    "/",
    "/((?!_next/static|_next/image|favicon.ico|assets).*)",
  ],
};
