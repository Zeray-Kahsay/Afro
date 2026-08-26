import { cn } from "@/@/lib/utils";
import { ArrowLeft } from "lucide-react";
import type { ReactNode } from "react";
import { Link, useNavigate } from "react-router-dom";

interface BackButtonProps {
    to:string;
    children?: ReactNode;
    className?: string;
    replace?: boolean;
    disabled?: boolean;
}

export function BackButton({ to, children, className, replace = false, disabled = false }: BackButtonProps) {
    const navigate = useNavigate();

    const classes = cn(
        "inline-flex items-center gap-2",
        "text-sm font-medium",
        "text-muted-foreground",
        "transition-colors duration-200",
        "text-indigo-500",
        "hover:text-indigo-800",
        "disabled:pointer-events-none disabled:opacity-50",
        className
    );

    
    if (to){
        return (
            <Link 
                to={to}
                replace={replace}
                className={classes}
                aria-disabled={disabled}
                onClick={e => {
                    if (disabled){
                        e.preventDefault();
                    }
                }}
            >
                <ArrowLeft className="size-4" />
                <span>{children}</span>
            </Link>
        );
    }


    return (
        <button
            type="button"
            disabled={disabled}
            className={classes}
            onClick={() => navigate(-1)}

        >
            <ArrowLeft className="size-4" />
        </button>
    )
}