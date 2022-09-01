<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { RelationCreateComponent, RelationDeleteComponent, RelationDetailsComponent,
RelationEditComponent, RelationIndexComponent } from "@/components/AdminRelated/RelationComponents"

export default{
    data(){
        return {
            componentName: 'RelationIndexComponent'
        }
    },
    components: {
        RelationCreateComponent,
        RelationDeleteComponent,
        RelationDetailsComponent,
        RelationEditComponent,
        RelationIndexComponent
    }
}
</script>