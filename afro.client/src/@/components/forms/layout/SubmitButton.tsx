import type { ReactNode } from "react";
import { Button } from "../../ui/button";


interface Props {
    loading: boolean;

    children: ReactNode;
}

export function SubmitButton({
    loading,
    children,
}: Props) {
    return (
        <Button
            type="submit"
            className="w-full"
            disabled={loading}
        >
            {loading
                ? "Saving..."
                : children}
        </Button>
    );
}