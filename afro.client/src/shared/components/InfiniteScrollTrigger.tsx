import { Loader2 } from "lucide-react";
import { useEffect, useRef } from "react";
import { Button } from "@/@/components/ui/button";

interface InfiniteScrollTriggerProps {
    hasMore: boolean;
    loading: boolean;
    error?: string;
    onLoadMore: () => void;
    onRetry?: () => void;
}

export function InfiniteScrollTrigger({
    hasMore,
    loading,
    error,
    onLoadMore,
    onRetry,
}: InfiniteScrollTriggerProps) {
    const triggerRef = useRef<HTMLDivElement | null>(null);

    useEffect(() => {
        const element = triggerRef.current;

        if (!element || !hasMore || loading || error) {
            return;
        }

        const observer = new IntersectionObserver(
            ([entry]) => {
                if (!entry.isIntersecting) {
                    return;
                }

                onLoadMore();
            },
            {
                root: null,
                rootMargin: "200px",
                threshold: 0,
            }
        );

        observer.observe(element);

        return () => {
            observer.disconnect();
        };
    }, [hasMore, loading, error, onLoadMore]);

    /*
     * Nothing more to load and there is no error to display.
     */
    if (!hasMore && !error) {
        return null;
    }

    return (
        <div
            ref={triggerRef}
            className="flex min-h-16 items-center justify-center py-3"
            aria-live="polite"
        >
            {loading && (
                <div className="flex items-center gap-2 text-sm text-muted-foreground">
                    <Loader2 className="h-4 w-4 animate-spin" />
                    <span>Loading more owners...</span>
                </div>
            )}

            {!loading && error && (
                <div className="flex flex-col items-center gap-2">
                    <p className="text-sm text-destructive">
                        {error}
                    </p>

                    {onRetry && (
                        <Button
                            type="button"
                            variant="outline"
                            size="sm"
                            onClick={onRetry}
                        >
                            Try again
                        </Button>
                    )}
                </div>
            )}
        </div>
    );
}