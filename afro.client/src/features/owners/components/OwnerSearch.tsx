import { Input } from "@/@/components/ui/input";
import { Search } from "lucide-react";

interface Props {
  value: string;
  onChange(value: string): void;
}

export function OwnerSearch({value, onChange}: Props){
  return (
    <div className="relative mb-6">
      <Search className="absolute right-3 top-1/2 h-5 w-5 -translate-y-1/2 text-muted-foreground"/>
      <Input 
        className="pr-10"
        placeholder="Search owner..."
        value={value}
        onChange={(e) => onChange(e.target.value)}
      />
    </div>
  )
}