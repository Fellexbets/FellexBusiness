<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { UserRelationIndexComponent, UserRelationCreateComponent, UserRelationDeleteComponent,
UserRelationDetailsComponent, UserRelationEditComponent } from '@/components/AdminRelated/UserRelationsComponents'

export default defineComponent({
    data(){
        return {
            componentName: 'UserRelationIndexComponent'
        }
    },
    components: {
        UserRelationIndexComponent,
        UserRelationCreateComponent,
        UserRelationDeleteComponent,
        UserRelationDetailsComponent,
        UserRelationEditComponent
    }
})
</script>