import { UserMenu } from "./UserMenu";

export function Header(){

  return (
    <header className="h-16 border-b bg-white flex items-center justify-between px-6">
      <h1 className="font-semibold">
        Admin Portal
      </h1>
      <UserMenu />
    </header>
  )
}