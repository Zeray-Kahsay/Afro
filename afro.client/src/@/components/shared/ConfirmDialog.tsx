import {
   AlertDialogAction, 
  AlertDialogFooter, 
  AlertDialogHeader,
  AlertDialog, 
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription, 
  AlertDialogTitle } from "../ui/alert-dialog";
import { Button } from "../ui/button";

interface ConfirmDialogProps {
  title: string;
  description: string;
  confirmText?: string;
  cancelText?: string;
  destructive?: boolean;
  loading?: boolean;
  open: boolean;
  onOpenChange(open: boolean): void;
  onConfirm(): Promise<void>;
}

export function ConfirmDialog({
  open,
  title,
  description,
  confirmText = "Confirm",
  cancelText = "Cancel",
  loading = false,
  destructive = false,
  onOpenChange,
  onConfirm
} : ConfirmDialogProps){
  
  return (
    <AlertDialog
      open={open}
      onOpenChange={onOpenChange}
    >
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>
            {title}
          </AlertDialogTitle>
          <AlertDialogDescription>
            {description}
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>
            {cancelText}
          </AlertDialogCancel>
          <AlertDialogAction asChild>
            <Button
              variant={
                destructive ? "destructive" : "default"
              }
              disabled={loading}
              onClick={onConfirm}
            >
              {loading ? "Please wait..." : confirmText}
            </Button>
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
