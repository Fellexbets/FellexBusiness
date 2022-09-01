<template>
    <NavBarComponent></NavBarComponent>
    <component :is="componentName" @change-page="(name : string) => componentName = name"></component>
</template>

<script lang="ts">
import { PlayedQuizzIndexComponent, PlayedQuizzCreateComponent, PlayedQuizzDetailsComponent,
PlayedQuizzEditComponent, PlayedQuizzDeleteComponent} from '@/components/AdminRelated/PlayedQuizzComponents'

export default{
    data(){
        return {
            componentName: 'PlayedQuizzIndexComponent'
        }
    },
    components: {
        PlayedQuizzIndexComponent,
        PlayedQuizzCreateComponent,
        PlayedQuizzDetailsComponent,
        PlayedQuizzEditComponent,
        PlayedQuizzDeleteComponent
    }
}
</script>