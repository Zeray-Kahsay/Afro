import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { loginSchema, type LoginFormValues } from "../validation/loginSchema";
import { useAppDispatch } from "../../../app/hooks";
import { useLoginMutation } from "../api/authApi";
import { setTokens } from "../slices/authSlice";
import { useNavigate } from "react-router-dom";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../../../@/components/ui/card";
import { Input } from "@/@/components/ui/input";
import { Button } from "@/@/components/ui/button";

export function LoginForm() {
  const { register, handleSubmit } = useForm<LoginFormValues>({
    resolver:
      zodResolver(loginSchema),
  });
  const dispatch = useAppDispatch();
  const [login, {isLoading}] = useLoginMutation();
  const navigate = useNavigate();


  const onSubmit =
    async (
      values: LoginFormValues
    ) => {
      try {
        const response = await login(values).unwrap();
        dispatch(setTokens({
          accessToken: response.accessToken,
          refreshToken: response.refreshToken
        })
      );
       navigate("/");
      } catch (error) {
        console.log(error);
      }
    };

  return (

    <Card className="w-full max-w-md shadow-sm">
      <CardHeader>
        <CardTitle>Welcome Back</CardTitle>
        <CardDescription>Sign in to continue</CardDescription>
      </CardHeader>
      <CardContent>
         <form
      onSubmit={
        handleSubmit(onSubmit)
      }
      className="space-y-4"
    >
      <Input
        {...register(
          "phoneNumber"
        )}
        placeholder="Phone Number"
      />

      <Input
        {...register(
          "password"
        )}
        type="password"
        placeholder="Password"
      />

      <Button
        className="w-full"
        disabled={isLoading}
        type="submit"
      >
        {isLoading ? "Logging in..." : "Login"}
      </Button>
    </form>
      </CardContent>
   
    </Card>
   
     
   
    
    
  );
}