<template>
    <div>
        <div class="search-wrapper">
            <h4>{{ $t('home.searchTitle') }}</h4>
            <div class="search-body">
                <div class="search-type">
                    <span :class="{'active': postTypeSearch == 1}" @click="postTypeSearch = 1; publicStore().setIsRenting(true)">{{ $t('header.rent') }}</span>
                </div>
                <hr/>
                <div class="search-content">
                    <div class="search-content-left">
                        <v-icon type="search-home"></v-icon>
                        <input 
                            class="search-input" 
                            :placeholder="$t('home.searchTitle')" 
                            v-model="searchValue"
                            @keydown.enter="search()"
                        />
                    </div>
                    <div>
                        <v-button
                            type="hasIconSecondary"
                            :label="$t('home.filter')"
                            icon="filter"
                            @click="common.showFilter()"
                        ></v-button>
                        <v-button
                            type="primary"
                            :label="$t('home.search')"
                            @click="search()"
                        ></v-button>
                    </div>
                    <div v-if="searchSuggestions.length > 0" class="search-suggest">
                        <div v-for="(item, index) in searchSuggestions" :key="index" @click="searchByAddress(item)">{{ item.Name }}</div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <h4 class="region-title">{{ $t('home.recomendTitle') }}</h4>
            <h3 class="region-sub-title" style="margin: 0 0 16px 0">{{ $t('home.recomendSubTitle') }}</h3>
            <div class="recomend">
                <div style="width: 98%;" class="card-wrapper" @click="navigateByCityName('6e52efd1-769c-2ea0-eedc-845b5dcdad45')">
                    <div class="recomend-card">
                        <img src="../../assets/image/dalat.jpg" alt="">
                    </div>
                    <h3>Hà Nội</h3>
                </div>
            </div>
        </div>
        <div>
            <h4 class="region-title">{{ $t('home.rentRealestateTtile') }}</h4>
            <h3 class="region-sub-title" style="margin: 0;">{{ $t('home.rentRealestateSubTitle') }}</h3>
            <Carousel :items-to-show="5">
                <Slide v-for="(realEstate, idx) in realEstateRent" :key="idx">
                    <RealEstateCard
                        :realEstate="realEstate"
                    ></RealEstateCard>
                </Slide>
                <template #addons>
                    <Navigation />
                    <Pagination />
                </template>
            </Carousel>
        </div>

        <div>
            <h4 class="region-title">{{ $t('home.buyRealestateTitle') }}</h4>
            <h3 class="region-sub-title" style="margin: 0;">{{ $t('home.buyRealestateSubTitle') }}</h3>
            <Carousel :items-to-show="5">
                <Slide v-for="(realEstate, idx) in realEstateBuy" :key="idx">
                    <RealEstateCard
                        :realEstate="realEstate"
                    ></RealEstateCard>
                </Slide>

                <template #addons>
                    <Navigation />
                    <Pagination />
                </template>
            </Carousel>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, reactive, inject, watch, ref } from 'vue';
import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'

import { getForCarousel } from '@/js/service/realEstate';
import { publicStore } from '@/js/store/publicStore';
import { getAddress } from '@/js/service/common';
import { debounce } from '@/js/common/helper';
import common from '@/js/common/helper';
import router from '@/js/router/router';
import RealEstateCard from '../components/RealEstateCard/RealEstateCard.vue';

publicStore().setIsHomePage(true);
let postTypeStore = publicStore().isRenting ? 1 : 2;

const postEnums = inject('$enums').postEnum;
const realEstateRent = reactive([]);
const realEstateBuy = reactive([]);
const searchSuggestions = reactive([]);
const address = [];
const searchValue = ref("");
const postTypeSearch = ref(postTypeStore);

onBeforeMount(async () => {
    const realEstates = await getForCarousel();
    realEstates.data.forEach(realEstate => {
        if (realEstate.Type == postEnums.RENT) {
            realEstateRent.push(realEstate)
        } else {
            realEstateBuy.push(realEstate)
        }
    })
    
    Object.assign(address, (await getAddress()).data)
})

async function navigateByCityName(id) {
    router.push({
        path: '/list',
        query: {
            provinceId: id,
            postType: postTypeSearch.value
        }
    })
}

const getSuggestion = debounce(() => {
    searchSuggestions.splice(0, searchSuggestions.length);
    if (searchValue.value.trim()) {
        address.forEach(item => {
            if (item.Name.includes(searchValue.value.trim())) searchSuggestions.push(item);
        })
    }
}, 300);

function search() {
    router.push({
        path: '/list',
        query: {
            search: searchValue.value,
            postType: postTypeSearch.value
        }
    })
}

function searchByAddress(address) {
    console.log(address);

    if (address.Type == 1) {
        router.push(`/list?provinceId=${address.Id}&postType=${postTypeSearch.value}`)
    } else {
        router.push(`/list?districtId=${address.Id}&postType=${postTypeSearch.value}`)
    }
}

watch(searchValue, () => {
    getSuggestion();
});

</script>

<style scoped>
@import url(./home-view.css);
.carousel__item {
  min-height: 200px;
  width: 100%;
  background-color: var(--color-primary);
  color: #fff;
  font-size: 20px;
  border-radius: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.carousel__slide {
  padding: 10px;
}

.carousel__prev,
.carousel__next {
  box-sizing: content-box;
  border: 5px solid white;
}
</style>