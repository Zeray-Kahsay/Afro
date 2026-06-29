import { LoginForm,} from "../components/LoginForm";

export function LoginPage() {
  return (
    <div
      className="
      min-h-screen
      bg-slate-50
      flex
      items-center
      justify-center
      px-4
      "
    >
        <LoginForm />
      
    </div>
  );
}