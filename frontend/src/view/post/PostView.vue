<template>
    <div class="post-wrapper">
        <h4 class="post-header">{{ $t('post.header') }}</h4>
        <hr/>
        <div>
            <h4 class="post-row-title">{{ $t('post.poster') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="personal" name="poster" value="0" checked v-model="isPersonal">
                    <label for="html">{{ $t('post.personal') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="agency" name="poster" value="1" v-model="isPersonal">
                    <label for="html">{{ $t('post.agency') }}</label><br>
                </div>
            </div>
        </div>
        <div class="post-row">
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.realestateType')"
                    :options="realestateOptions"
                    v-model="realestateType"
                    required
                    selectOnly
                ></v-combobox>
            </div>
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.postType')"
                    :options="postOptions"
                    v-model="postType"
                    required
                    selectOnly
                ></v-combobox>
            </div>
        </div>
        <div class="post-row">
            <v-input
                w100
                :label="$t('post.realestateName')"
                v-model="realestateName"
            ></v-input>
        </div>
        <div class="post-row">
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.chooseProvince')"
                    :options="provincesOptions"
                    v-model="province"
                    required
                    selectOnly
                    @selectedItem="handleSelectedProvince"
                ></v-combobox>
            </div>
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.chooseDistrict')"
                    :options="districtOptions"
                    v-model=district
                    required
                    selectOnly
                ></v-combobox>
            </div>
        </div>
        <div class="post-row">
            <v-input
                w100
                :label="$t('post.addressDetail')"
                required
                v-model="addressDetail"
            ></v-input>
            <div style="margin: 28px 0 0 20px">
                <v-button
                    :label="$t('post.chooseFromMap')"
                    type="primary"
                    @click="showGoogleMap = true"
                ></v-button>
            </div>
        </div>
        <div v-show="showGoogleMap" class="google-map-wrapper">
            <div class="google-map-content">
                <div class="google-map-header">
                    <div></div>
                    <h5>{{ $t('post.choosePosition') }}</h5>
                    <v-icon type="close" @click="showGoogleMap = false"></v-icon>
                </div>
                <div class="google-map-center">
                    <GoogleMap
                        api-key="AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg"
                        style="width: 1200px; height: 500px"
                        :center="positionLatLng"
                        :zoom="15"
                        @click="handleClick"
                    >
                        <Marker :options="{ position: positionLatLng }" />
                    </GoogleMap>
                </div>
                <div class="google-map-footer">
                    <div>
                        <span>Kinh độ: {{ positionLatLng.lng }}</span>
                        <span>-</span>
                        <span>Vĩ độ: {{ positionLatLng.lat }}</span>
                    </div>
                    <div>
                        <v-button
                            :label="$t('button.cancel')"
                            type="secondary"
                            @click="showGoogleMap = false"
                        ></v-button>
                        <v-button
                            :label="$t('button.save')"
                            type="primary"
                            @click="savePos"
                        ></v-button>
                    </div>
                </div>
            </div>
        </div>
        <div class="post-row">
            <div style="width: 47%">
                <v-input
                    w100
                    :label="$t('post.area')"
                    required
                    typeNumber
                    v-model="area"
                ></v-input>
            </div>
            <div style="width: 47%">
                <v-input
                    w100
                    :label="$t('post.price')"
                    v-model="price"
                    required
                    typeNumber
                ></v-input>
            </div>
        </div>
        <div class="post-row">
            <v-input
                w100
                :label="$t('post.title')"
                required
                v-model="title"
            ></v-input>
        </div>
        <div class="post-row post-description">
            <label class='input--required'>{{ $t('post.description') }}</label>
            <textarea v-model="description"></textarea>
        </div>

        <div v-if="realestateType == realestateEnum.HOUSE">
            <div class="post-row">
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfBedRoom')"
                        required
                        typeNumber
                        v-model="houseNumberOfBedRoom"
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfToilet')"
                        required
                        typeNumber
                        v-model="houseNumberOfToilet"
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfFloor')"
                        required
                        typeNumber
                        v-model="houseNumberOfFloor"
                    ></v-input>
                </div>
            </div>
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.funiture')"
                    required
                    v-model="houseFuniture"
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.redBook') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="hasRedBook" name="redBook" value="1" checked v-model="houseRedBook">
                    <label for="html">{{ $t('post.hasRedBook') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="agenoRedBookncy" name="redBook" value="0" v-model="houseRedBook">
                    <label for="html">{{ $t('post.noRedBook') }}</label><br>
                </div>
            </div>
        </div>
        <div v-else-if="realestateType == realestateEnum.BOARDINGHOUSE">
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.funiture')"
                    required
                    v-model="boardingHouseFuniture"
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.selfContained') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="private" name="selfContained" value="1" checked v-model="boardingHouseSelfContained">
                    <label for="html">{{ $t('post.private') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="shard" name="selfContained" value="0" v-model="boardingHouseSelfContained">
                    <label for="html">{{ $t('post.shard') }}</label><br>
                </div>
            </div>
        </div>
        <div v-else-if="realestateType == realestateEnum.APARTMENT">
            <div class="post-row">
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfBedRoom')"
                        required
                        typeNumber
                        v-model="apartmentNumberOfBedRoom"
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfToilet')"
                        required
                        typeNumber
                        v-model="apartmentNumberOfToilet"
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.floor')"
                        required
                        typeNumber
                        v-model="apartmentFloor"
                    ></v-input>
                </div>
            </div>
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.funiture')"
                    required
                    v-model="apartmentFuniture"
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.legalDocument') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="pinkBook" name="legalDocument" value="1" checked v-model="apartmentLegalDocument">
                    <label for="html">{{ $t('post.pinkBook') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="noHave" name="legalDocument" value="0" v-model="apartmentLegalDocument">
                    <label for="html">{{ $t('post.noHave') }}</label><br>
                </div>
            </div>
        </div>
        <div v-else-if="realestateType == realestateEnum.LAND">
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.landType')"
                    required
                    v-model="landType"
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.legalDocument') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="have" name="legalDocumentLand" value="1" checked v-model="landLegalDocument">
                    <label for="html">{{ $t('post.have') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="noHave" name="legalDocumentLand" value="0" v-model="landLegalDocument">
                    <label for="html">{{ $t('post.noHave') }}</label><br>
                </div>
            </div>
        </div>
        <div class="import-file-header">
            <h4 class="import-file-title input--required">{{ $t('post.provideImage') }}</h4>
            <v-button
                v-if="images.length > 0 || imagesCloud.length > 0"
                :label="$t('post.addImage')"
                type="hasIconPrimary"
                icon="add"
                @click="$refs.inputFileRef.click()"
            ></v-button>
        </div>
        <div @dragover.prevent @drop.prevent="e => dragFile(e.dataTransfer.files)">
            <input 
                type="file" 
                ref="inputFileRef" 
                v-show="false" 
                accept=".png, .jpg"
                multiple
                @input="dragFile($event.target.files)" 
            />
            <div v-if="images.length > 0 || imagesCloud.length > 0" class="import-file-wrapper--has-image">
                <div class="img-import-wrapper">
                    <div v-for="(preview, index) in images" :key="index">
                        <div class="img-import-icon-wrapper">
                            <v-icon type="remove" @click="removeImage(index)" :desc="$t('post.removeImage')"></v-icon>
                        </div>
                        <img :src="preview" alt="Preview" class="img-import">
                    </div>
                    <div v-for="(item, index) in imagesCloud" :key="index">
                        <div class="img-import-icon-wrapper">
                            <v-icon type="remove" @click="removeImage(index, true)" :desc="$t('post.removeImage')"></v-icon>
                        </div>
                        <img :src="item" alt="Preview" class="img-import">
                    </div>
                </div>
            </div>
            <div class="import-file-wrapper" v-else>
                <div class="import-file" @click="$refs.inputFileRef.click()"></div>
                <p>{{ $t('post.dragFile') }}</p>
                <p>{{ $t('post.supportFile') }}</p>
            </div>
        </div>
        <div style="display: flex; justify-content: flex-end;">
            <v-button
                :label="!isEdit ? $t('post.post') : 'Lưu thay đổi'"
                type="hasIconPrimary"
                icon="checked"
                @click="createNewPost"
            ></v-button>
        </div>
        <!-- api-key="AIzaSyAI9kPkskayYti5ttrZL_UfBlL3OkMEbvs" -->
    </div>
</template>

<script setup>
import { ref, inject, onBeforeMount, reactive } from 'vue';
import { GoogleMap, Marker } from 'vue3-google-map';
import { ref as storageRef } from 'firebase/storage';
import { useFirebaseStorage, useStorageFile, useStorageFileUrl } from 'vuefire';
// import axios from "axios";

import common from '@/js/common/helper';
import { getRecord, getRecords, updateRecord } from '@/js/service/base';
import { getDistrictsByProvinceId } from '@/js/service/district';
import { createRecord } from '@/js/service/base';
import { publicStore } from '@/js/store/publicStore';
import { getUserInfo } from '@/js/service/auth';
import { router } from '@/js/router/router';
import { deFormatNumber } from '@/js/common/helper';
import enums from '@/js/common/enum';
import { getDetailRealEstate } from '@/js/service/realEstate';

const apartment = reactive({});
const boardingHouse = reactive({});
const house = reactive({});
const land = reactive({});
const imagesCloud = reactive([]);
const isEdit = ref(false);
const objectId = ref('');

const storage = useFirebaseStorage()

const isPersonal = ref(true);
const realestateType = ref(null)
const postType = ref(null)
const realestateName = ref('')
const province = ref(null)
const district = ref(null)
const addressDetail = ref('')
const area = ref(null)
const price = ref(null)
const title = ref('')
const description = ref('')
const houseNumberOfBedRoom = ref(null)
const houseNumberOfToilet = ref(null)
const houseNumberOfFloor = ref(null)
const houseFuniture = ref('')
const houseRedBook = ref(false)
const boardingHouseFuniture = ref('')
const boardingHouseSelfContained = ref(false)
const apartmentNumberOfBedRoom = ref(null)
const apartmentNumberOfToilet = ref(null)
const apartmentFloor = ref(null)
const apartmentFuniture = ref('')
const apartmentLegalDocument = ref(false)
const landType = ref('')
const landLegalDocument = ref(false)
const positionLatLng = reactive({
    lat: 20.993302571091153,
    lng: 105.84508713545992
})
const position = reactive({
    lat: 0,
    lng: 0
})
const statusEnum = inject('$enums').statusEnum;
const images = reactive([]);
const imageFiles = reactive([]);
const showGoogleMap = ref(false);
const realestateEnum = inject('$enums').realEstateEnum;
const realestateOptions = [
    {
        title: "Nhà mặt đất",
        value: realestateEnum.HOUSE
    },
    {
        title: "Phòng trọ",
        value: realestateEnum.BOARDINGHOUSE
    },
    {
        title: "Chung cư",
        value: realestateEnum.APARTMENT
    },
    {
        title: "Đất",
        value: realestateEnum.LAND
    }
]

const postEnum = inject('$enums').postEnum;
const postOptions = [
    {
        title: "Buôn bán",
        value: postEnum.BUY
    },
    {
        title: "Cho thuê",
        value: postEnum.RENT
    }
]

const provincesOptions = reactive([]);
const districtOptions = reactive([]);
let ownerId;

onBeforeMount(async () => {
    if (!publicStore().isAuthPage) {
        const user = await getUserInfo();
        if (!user.data) {
            router.push('/login?returnUrl=post');
            return;
        } else {
            if (user.data.Role == 1) {
                common.showDialog(enums.statusEnum.ERROR, "Cảnh báo", ["Vui lòng không dùng tài khoản admin để đăng bài"]);
                router.push("/");
            }           

            ownerId = user.data.Id;
        }
    }

    const provinceList = await getRecords('Province')
    provinceList.data?.forEach(province => provincesOptions.push({
        title: province.Name,
        value: province.Id
    }))

    if( navigator.geolocation )
    {
        navigator.geolocation.getCurrentPosition( success, fail );
    }
    else
    {
        alert("Sorry, your browser does not support geolocation services.");
    }

    const urlParams = new URLSearchParams(window.location.search);
    const postId = urlParams.get('postId')
    if (postId) {
        let realEstatePosted = await getRecord('RealEstate', postId);
        realEstatePosted = realEstatePosted.data;

        if (realEstatePosted) {
            isEdit.value = true;
            isPersonal.value = realEstatePosted.IsPersonal;
            realestateType.value = realEstatePosted.RealEstateType;
            postType.value = realEstatePosted.Type;
            realestateName.value = realEstatePosted.Name;
            addressDetail.value = realEstatePosted.Address;
            area.value = realEstatePosted.Area;
            price.value = realEstatePosted.Price;
            title.value = realEstatePosted.Title;
            description.value = realEstatePosted.Description;
            positionLatLng.lat = realEstatePosted.Latitude;
            positionLatLng.lng = realEstatePosted.Longtitude;

            imagesCloud.splice(0, imagesCloud.length);
            realEstatePosted.ImageUrls.forEach(i => imagesCloud.push(i.Url))
            switch (realEstatePosted.RealEstateType) {
                case enums.realEstateEnum.HOUSE:
                    Object.assign(house, (await getDetailRealEstate('House', realEstatePosted.Id)).data);
                    console.log(house);
                    objectId.value = house.Id;
                    console.log(objectId.value);
                    province.value = house.RealEstateDto.District.ProvinceId;
                    await handleSelectedProvince({ value: province.value })
                    district.value = house.RealEstateDto.District.Id;

                    houseNumberOfBedRoom.value = house.NumberOfBedRoom
                    houseNumberOfToilet.value = house.NumberOfToilet
                    houseNumberOfFloor.value = house.NumberOfFloor
                    houseFuniture.value = house.Funiture
                    houseRedBook.value = house.RedBook ? "1" : "0"
                    break;
                case enums.realEstateEnum.APARTMENT:
                    Object.assign(apartment, (await getDetailRealEstate('Apartment', realEstatePosted.Id)).data);
                    objectId.value = apartment.Id;
                    province.value = apartment.RealEstateDto.District.ProvinceId;
                    await handleSelectedProvince({ value: province.value })
                    district.value = apartment.RealEstateDto.District.Id;

                    apartmentNumberOfBedRoom.value = apartment.NumberOfBedRoom
                    apartmentNumberOfToilet.value = apartment.NumberOfToilet
                    apartmentFloor.value = apartment.Floor
                    apartmentFuniture.value = apartment.Funiture
                    apartmentLegalDocument.value = apartment.LegalDocument
                    break;
                case enums.realEstateEnum.BOARDINGHOUSE: 
                    Object.assign(boardingHouse, (await getDetailRealEstate('BoardingHouse', realEstatePosted.Id)).data);
                    objectId.value = boardingHouse.Id;
                    province.value = boardingHouse.RealEstateDto.District.ProvinceId;
                    await handleSelectedProvince({ value: province.value })
                    district.value = boardingHouse.RealEstateDto.District.Id;

                    boardingHouseFuniture.value = boardingHouse.Funiture;
                    boardingHouseSelfContained.value = boardingHouse.SelfContained;
                    break;
                case enums.realEstateEnum.LAND:
                    Object.assign(land, (await getDetailRealEstate('Land', realEstatePosted.Id)).data);
                    objectId.value = land.Id;
                    province.value = land.RealEstateDto.District.ProvinceId;
                    await handleSelectedProvince({ value: province.value })
                    district.value = land.RealEstateDto.District.Id;

                    landType.value = land.Type
                    landLegalDocument.value = land.LegalDocument
                    break;
                default: 
                    break;
            }
        }
    }
});

async function handleSelectedProvince(item) {
    const res = await getDistrictsByProvinceId(item.value)
    districtOptions.splice(0, districtOptions.length)
    res.data.forEach(district => districtOptions.push({
        title: district.Name,
        value: district.Id
    }));
}

function handleClick(e) {
    positionLatLng.lat = e.latLng.lat();
    positionLatLng.lng = e.latLng.lng();
}

function success(position) {
    positionLatLng.lat = position.latitude;
    positionLatLng.lng = position.longitude;
}

function fail() {
    // Could not obtain location
}

function savePos() {
    position.lng = positionLatLng.lng;
    position.lat = positionLatLng.lat;
    showGoogleMap.value = false;
}

function dragFile(files) {
    for (let i = 0; i < files.length; i++) {
        let file = files[i];
        imageFiles.push(file)
        if (file) {
            let reader = new FileReader();
            reader.onload = e => images.push(e.target.result)
            reader.readAsDataURL(file);
        }
    }
}

function removeImage(idx, isCloud) {
    if (isCloud) {
        imagesCloud.splice(idx, 1);
    } else {
        images.splice(idx, 1);
        imageFiles.splice(idx, 1);
    }
}

function valideHouseProperties() {
    if (!houseNumberOfBedRoom.value
        || !houseNumberOfToilet.value
        || !houseNumberOfFloor.value
        || !houseFuniture.value
    ) return false;
    return true;
}

function validateBoardingHouseProperties() {
    if (!boardingHouseFuniture.value) return false;
    return true;
}

function validateApartmentProperties() {
    if (!apartmentNumberOfBedRoom.value
        || !apartmentNumberOfToilet.value
        || !apartmentFloor.value
        || !apartmentFuniture.value
    ) return false;
    return true;
}

function validateLandProperties() {
    if (!landType.value) return false;
    return true;
}

function valideProperties() {
    if (!realestateType.value 
        || !postType.value
        || !province.value
        || !district.value
        || !area.value
        || !price.value
        || !title.value
        || !description.value
        || (realestateType.value == realestateEnum.HOUSE && !valideHouseProperties())
        || (realestateType.value == realestateEnum.BOARDINGHOUSE && !validateBoardingHouseProperties)
        || (realestateType.value == realestateEnum.APARTMENT && !validateApartmentProperties())
        || (realestateType.value == realestateEnum.LAND && !validateLandProperties())
    ) {
        common.showDialog(statusEnum.WARNING, "Cảnh báo", ["Vui lòng nhập đủ thông tin!"])
        return false;
    }

    if (images.length + imagesCloud.length < 4) {
        common.showDialog(statusEnum.WARNING, "Cảnh báo", ["Cung cấp đủ 4 hình ảnh trở lên"])
        return false;
    }

    return true;
}

async function createNewPost() { 

    if (valideProperties()) {
        let object;
        let record;
        let RealEstateCreateDto = {
            IsPersonal: isPersonal.value,
            OwnerId: ownerId,
            DistrictId: district.value,
            Address: addressDetail.value,
            Latitude: position.lat,
            Longtitude: position.lng,
            Area: parseFloat(area.value),
            Title: title.value,
            Name: realestateName.value,
            Description: description.value,
            Price: parseFloat(deFormatNumber(price.value)),
            Type: postType.value
        }

        switch (realestateType.value) {
            case realestateEnum.HOUSE:
                object = 'House';
                RealEstateCreateDto.RealEstateType = realestateEnum.HOUSE;
                record = {
                    RealEstateCreateDto,
                    NumberOfBedRoom: parseInt(houseNumberOfBedRoom.value),
                    NumberOfToilet: parseInt(houseNumberOfToilet.value),
                    NumberOfFloor: parseInt(houseNumberOfFloor.value),
                    Funiture: houseFuniture.value,
                    RedBook: houseRedBook.value == "1"
                }
                break;
            
            case realestateEnum.BOARDINGHOUSE:
                object = 'BoardingHouse';
                RealEstateCreateDto.RealEstateType = realestateEnum.BOARDINGHOUSE;
                record = {
                    RealEstateCreateDto,
                    Funiture: boardingHouseFuniture.value,
                    SeftContained: boardingHouseSelfContained.value == "1"
                }
                break;

            case realestateEnum.APARTMENT:
                object = 'Apartment';
                RealEstateCreateDto.RealEstateType = realestateEnum.APARTMENT;
                record = {
                    RealEstateCreateDto,
                    NumberOfBedRoom: parseInt(apartmentNumberOfBedRoom.value),
                    NumberOfToilet: parseInt(apartmentNumberOfToilet.value),
                    Floor: parseInt(apartmentFloor.value),
                    LegalDocument: apartmentLegalDocument.value == "1"
                }
                break;

            case realestateEnum.LAND:
                object = 'Land';
                RealEstateCreateDto.RealEstateType = realestateEnum.LAND;
                record = {
                    RealEstateCreateDto,
                    LandType: landType.value,
                    LegalDocument: landLegalDocument.value == "1"
                }
                break;

            default: 
                break;
        }
        common.showLoading(true);

        let imageUrls = [];
        for (const [idx, image] of imageFiles.entries()) {
            let imageName = Date.now().toString() + idx + image.name;
            let imageRef = storageRef(storage, `images/${imageName}`)
            let { upload } = useStorageFile(imageRef)

            await upload(image);

            const {
                refresh
            } = useStorageFileUrl(imageRef)
            const imgRefresh = await refresh();
            imageUrls.push(imgRefresh)
        }
        
        record.RealEstateCreateDto.ImageUrlsCreateDto = imageUrls.map(url => ({
            Url: url
        }));

        imagesCloud.forEach(i => record.RealEstateCreateDto.ImageUrlsCreateDto.push({ Url: i }));
        let result = null;

        if (!isEdit.value) {
            result = await createRecord(object, record);
            console.log(record);

            if (result.data) {
                window.location.href = `http://localhost:8888/order/create_payment_url?realEstateId=${result.data}`;
            }
        } else {
            const urlParams = new URLSearchParams(window.location.search);
            const postId = urlParams.get('postId');
            console.log(record);
            record.RealEstateUpdateDto = record.RealEstateCreateDto;
            record.RealEstateId = postId;
            console.log(record);

            result = await updateRecord(object, objectId.value, record)
            console.log(result);

        }

        common.showLoading(false);
        if (result.data) {
            if (isEdit.value) {
                common.showDialog(enums.statusEnum.SUCCES0, "Thành công", [`${isEdit.value ? 'Chỉnh sửa' : 'Thêm mới'} thành công!`], () => window.location.href = '/')
            }
        } else {
            common.showDialog(enums.statusEnum.ERROR, "Lỗi", ["Có lỗi xảy ra!"])
        }
    }
}
</script>

<style scoped>
@import url(./post-view.css);
</style>