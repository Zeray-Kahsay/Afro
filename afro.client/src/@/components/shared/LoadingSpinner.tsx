import { Loader2 } from "lucide-react";

interface LoadingSpinnerProps {
    size?: number;
}

export function LoadingSpinner({
    size = 32,
}: LoadingSpinnerProps) {
    return (
        <div
            className="
            flex
            items-center
            justify-center
            py-10
            "
        >
            <Loader2
                className="animate-spin"
                style={{
                    width: size,
                    height: size,
                }}
            />
        </div>
    );
}
