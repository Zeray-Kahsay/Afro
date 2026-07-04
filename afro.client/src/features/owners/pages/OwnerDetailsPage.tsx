import { Link, useParams } from "react-router-dom";
import { useGetOwnerQuery } from "../api/ownerApi";
import { BackButton, EmptyState, LoadingSpinner, PageContainer, PageHeader } from "@/@/components/shared";
import { OwnerDetailsCard } from "../components/OwnerDetailsCard";
import { Button } from "@/@/components/ui/button";


export function OwnerDetailsPage() {
const {ownerId } = useParams();
const {data: owner, isLoading, isError } = useGetOwnerQuery(ownerId!)

if (isLoading){
    return <LoadingSpinner />;
}

if (isError || !owner){
    return (
        <EmptyState 
            title="Owner not found"
            description="The requested owner could not be found."
        />
    )
}
    return (
        <PageContainer>
            <PageHeader 
                title="Owner Details"
                description="View owner information"
                navigation={
                    <BackButton to="/owners" replace>
                        Back to Owners
                    </BackButton>
                }
                action={
                    <Button asChild>
                        <Link to={`/owners/${owner.id}/edit`}>
                            Edit Owner
                        </Link>
                    </Button>
                }
            />
            <OwnerDetailsCard owner={owner} />
        </PageContainer>
    );
}