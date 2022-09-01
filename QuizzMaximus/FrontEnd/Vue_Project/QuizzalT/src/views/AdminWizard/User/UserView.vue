<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { UserCreateComponent, UserIndexComponent, UserDetailsComponent,
UserDeleteComponent, UserEditComponent} from "@/components/AdminRelated/UserComponents";

export default defineComponent({
    data(){
        return {
            componentName: 'UserIndexComponent'
        }
    },
    components: {
        UserCreateComponent,
        UserIndexComponent,
        UserDetailsComponent,
        UserDeleteComponent,
        UserEditComponent
    }
})
</script>