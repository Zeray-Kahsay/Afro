
interface AvatarProps {
    name: string;
}

function getInitials(name: string){
    return name.split(" ").filter(Boolean).slice(0,2).map(x => x[0].toUpperCase()).join("");
}

export function Avatar ({name}: AvatarProps) {
    return (
        <div className="flex h-20 w-20 items-center justify-center rounded-full bg-primary/10 text-2xl font-bold text-primary">
            {getInitials(name)}
        </div>
    );
}