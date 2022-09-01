<template>
    <div>
        <select class="dropdown col-2" v-model="filter" :key="filter" @change="filterQuestions(filter)"> 
            <option v-bind:value="null_value">Filter by Theme</option>
            <option v-for="filter in filters" :key="filter">{{ filter }}</option>
        </select>

        <select class="dropdown col-2" v-model="sort" :key="sort" @change="sortQuestions(sort)">
            <option v-bind:value="null_value">Sort</option>
            <option v-for="sort in sorting" :key="sort">{{ sort }}</option>
        </select>

        <label for="search">Search:
            <input class="searchbar" id="search" v-model="term" @keypress.enter="search(term)"/>
        </label>
    </div>
</template>

<script lang="ts">
const filters = [
    'All',
    'Art',
    'History',
    'Science',
    'Math',
    'English',
    'Literature',
    'Astronomy',
    'Biology',
    'Chemistry',
    'Geography',
    'Movies',
    'Music',
    'Physics',
    'Wild Life'
]

const sorting = [
    'UserId',
    'QuestionId',
    'ThemeId'
]

export default {
    props: [
        'filterQuestions',
        'sortQuestions',
        'search',
        'filter',
        'sort',
        'null_value'
    ],
    data() {
        return {
            filters,
            sorting,
            term: ''
        }
    }
}
</script>