<template>
    <NavBarComponent></NavBarComponent>
    <component :is="currentComponent" @change-page="(name : string) => currentComponent = name"></component>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { MyQuestionsComponent, MyQuestionsCreateComponent, MyQuestionsDetailsComponent,
MyQuestionsEditComponent } from "@/components/MyAreaComponents/MyQuestionsComponents";

export default defineComponent({
    data(){
        return {
            currentComponent: 'MyQuestionsComponent'
        }
    },
    components: {
        MyQuestionsComponent,
        MyQuestionsCreateComponent,
        MyQuestionsDetailsComponent,
        MyQuestionsEditComponent
    }
})
</script>