import { Card, CardContent, CardHeader, CardTitle } from "@/@/components/ui/card";
import type { Owner } from "../types/Owner";
import { InfoRow } from "@/@/components/shared/InfoRow";
import { Separator } from "@/@/components/ui/separator";
import { Avatar } from "@/@/components/shared/Avatar";
import { DetailsSection } from "@/@/components/shared/DetailsSection";
import { Mail, MapPin, NotebookPen, Phone } from "lucide-react";

interface OwnerDetailsCardProps {
    owner: Owner;
}

export function OwnerDetailsCard({owner}: OwnerDetailsCardProps){
    return (
        <div className="grid grid-6 lg:grid-cols-2 gap-0.5">
            <Card>
                <div className="mb-8 flex flex-col items-center">
                    <Avatar  name={owner.fullName}/>
                <h2 className="mt-4 text-2xl font-bold text-center">
                    {owner.fullName}
                </h2>
                <p className="text-muted-foreground">
                    Property Owner
                </p>
                </div>
              
                <DetailsSection 
                    title="Contact Information"
                    description="Primary contact details"
                    
                >

                <CardContent>
                    <InfoRow 
                        icon={<NotebookPen className="size-4 " />}
                        label="Full Name"
                        value={owner.fullName}
                    />

                    <Separator />

                    <InfoRow 
                        icon={<Phone className="size-4 " />}
                        label="Phone"
                        value={owner.phoneNumber}
                    />

                    <Separator />

                    <InfoRow 
                        icon={<Mail className="size-4 " />}
                        label="Email"
                        value={owner.email}
                    />

                </CardContent>

                </DetailsSection>
            </Card>
            <Card>
            
                <DetailsSection
                    title="Additional Information"
                    description="Other details about the owner"
                >
                <CardContent>
                    <InfoRow 
                        icon={<MapPin className="size-4 " />}
                        label="Address"
                        value={owner.address ?? "No address available."}
                    />
                    <Separator />
                    <InfoRow 
                        icon={<NotebookPen className="size-4 " />}
                        label="Notes"
                        value={owner.notes ?? "No notes available."}
                    />
                </CardContent>

                </DetailsSection>
            </Card>
        </div>     
       
    );
}