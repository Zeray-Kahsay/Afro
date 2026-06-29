import { useNavigate } from "react-router-dom"
import { useAppDispatch } from "../../../app/hooks";
import { useLogoutMutation } from "../api/authApi";
import { logout } from "../slices/authSlice";
import { Button } from "@/@/components/ui/button";

export function LogoutButton(){
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const [logoutRequest] = useLogoutMutation();

  const handleLogout = async () => {
    try {
      await logoutRequest().unwrap();
    } catch {
      
    }

    dispatch(logout());
    navigate("/login");
  }


  return(
    <Button onClick={handleLogout}>
      Logout
    </Button>
  )
}
